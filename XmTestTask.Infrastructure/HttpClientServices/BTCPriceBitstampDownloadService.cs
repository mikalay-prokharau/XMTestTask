using Newtonsoft.Json.Linq;
using XmTestTask.Core.Helpers;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Infrastructure.HttpClientServices
{
    public class BTCPriceBitstampDownloadService : BTCPriceDownloadBaseService, IBTCPriceDownloadService
    {
        public BTCPriceBitstampDownloadService(HttpClient client) : base(client, "Bitstamp") 
        {
        }

        protected override decimal? getPriceFromResponse(string? requestResult)
        {
            if (requestResult == null)
                return null;

            var jsonResult = JObject.Parse(requestResult);
            var dataJArray = jsonResult?["data"]?["ohlc"];
            if (dataJArray == null || !dataJArray.HasValues || !(dataJArray is JArray))
                return null;

            var priceJToken = dataJArray[0]?["close"];
            if (priceJToken == null || !decimal.TryParse(priceJToken.ToString(), out var price))
                return null;

            return price;
        }

        protected override string getAdditionalURL(int date)
        {
            var startDate = DateHelper.ConvertIntDateToUnix(date);
            return $"?step=3600&limit=1&start={startDate}";
        }
    }
}
