using XmTestTask.Core.Entities;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Core.Services
{
    public class BTCPriceAverageCalculationService : IBTCPriceCalculationService
    {
        public decimal Calculate(IEnumerable<BTCPrice> prices)
        {
            return Math.Round(prices.Average(b => b.Price), 2);
        }
    }
}
