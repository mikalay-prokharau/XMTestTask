using Autofac;
using XmTestTask.Core.Interfaces;
using XmTestTask.Core.Services;

namespace XmTestTask.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<BTCPriceAverageCalculationService>()
                .As<IBTCPriceCalculationService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BTCPriceService>()
                .As<IBTCPriceService>()
                .InstancePerLifetimeScope();
        }
    }
}
