using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using XmTestTask.Core.Helpers;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Infrastructure.HttpClientServices
{
    public class BTCPriceBitfinexDownloadService : BTCPriceDownloadBaseService, IBTCPriceDownloadService
    {
        public BTCPriceBitfinexDownloadService(HttpClient client) : base(client, "Bitfinex")
        {
        }

        protected override decimal? getPriceFromResponse(string? requestResult)
        {
            if (requestResult == null)
                return null;

            var valuesString = Regex.Match(requestResult, @"\[\[.*?\]\]").Groups[0].Value;
            if (valuesString == null)
                return null;

            var values = valuesString.Split(',');
            if (values.Length < 3 || !decimal.TryParse(values[2], out var price))
                return null;

            return price;
        }

        protected override string getAdditionalURL(int date)
        {
            var startDate = DateHelper.ConvertIntDateToUnixInMilliseconds(date);
            var endDate = startDate + TimeSpan.FromHours(1).TotalMilliseconds;
            return $"?start={startDate}&end={endDate}&limit=1";
        }
    }
}
