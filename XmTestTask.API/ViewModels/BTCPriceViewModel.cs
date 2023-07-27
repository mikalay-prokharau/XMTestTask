using XmTestTask.Core.Entities;
using XmTestTask.Core.Helpers;

namespace XmTestTask.API.ViewModels
{
    public class BTCPriceViewModel
    {
        /// <summary>
        /// Unix date
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// BTC price
        /// </summary>
        public decimal Price { get; set; }

        public static BTCPriceViewModel FromBTCPrice(BTCPrice btcPrice)
        {
            return new BTCPriceViewModel()
            {
                Date = DateHelper.ConvertIntDateToUnixInMilliseconds(btcPrice.Date),
                Price = btcPrice.Price
            };
        }
    }
}
