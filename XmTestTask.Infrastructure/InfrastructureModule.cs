using System.Net.Http;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using XmTestTask.Core.Entities;
using XmTestTask.Core.Interfaces;
using XmTestTask.Infrastructure.Data;
using XmTestTask.Infrastructure.HttpClientServices;
using Module = Autofac.Module;

namespace TestTaskTemplate.Infrastructure;
public class InfrastructureModule : Module
{
    private readonly IConfiguration configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
        .RegisterType<BTCRepository>()
        .As<IBTCRepository>()
        .InstancePerLifetimeScope();

        builder
        .RegisterType<BTCRepository>()
        .As<IBTCRepository>()
        .InstancePerLifetimeScope();

        var bitstampUrl = configuration.GetSection("DownloadPriceUrls").GetSection("Bitstamp").Value;
        var bitfinexUrl = configuration.GetSection("DownloadPriceUrls").GetSection("Bitfinex").Value;

        RegisterHttpClientService<BTCPriceBitstampDownloadService>(bitstampUrl!, builder);
        RegisterHttpClientService<BTCPriceBitfinexDownloadService>(bitfinexUrl!, builder);
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                        retryAttempt)));
    }

    private static void RegisterHttpClientService<T>(string url, ContainerBuilder builder) where T: class, IBTCPriceDownloadService
    {
        builder.Register(_ =>
        {
            var services = new ServiceCollection();
            services.AddHttpClient<IBTCPriceDownloadService, T>(client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("accept", "application/json");
            })
            .AddPolicyHandler(GetRetryPolicy());

            return services.BuildServiceProvider().GetRequiredService<IBTCPriceDownloadService>();
        });
    }
}
