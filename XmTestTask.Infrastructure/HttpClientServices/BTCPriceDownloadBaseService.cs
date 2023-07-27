using Autofac.Core;
using Newtonsoft.Json.Linq;
using XmTestTask.Core.Entities;
using XmTestTask.Core.Exceptions;

namespace XmTestTask.Infrastructure.HttpClientServices
{
    public abstract class BTCPriceDownloadBaseService : HttpClientBaseService
    {
        private readonly string serviceName = string.Empty;

        public BTCPriceDownloadBaseService(HttpClient client, string serviceName) : base(client)
        {
            this.serviceName = serviceName;
        }

        public async Task<BTCPrice> GetBTCPrice(int date, CancellationToken cancelationToken = default)
        {
            var requestResult = await SendRequest(getAdditionalURL(date), cancelationToken);

            var price = getPriceFromResponse(requestResult);
            if (price == null)
                throw new DataRetrivalException($"Data retrieval exception from the {serviceName} service");

            return new BTCPrice() { Price = price.Value, Date = date };
        }

        protected abstract decimal? getPriceFromResponse(string? requestResult);

        protected abstract string getAdditionalURL(int date);
    }
}
