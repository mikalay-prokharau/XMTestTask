namespace XmTestTask.Core.Entities
{
    /// <summary>
    /// Class <c>BTCPrice</c> represents BTC/USD price entity.
    /// </summary>
    public class BTCPrice
    {
        /// <value>
        /// Property <c>Date</c> represents date and time in int format with accuracy to a hour and used as Id.
        /// </value>
        public int Date { get; set; }

        /// <value>
        /// Property <c>Price</c> represents BTC/USD price.
        /// </value>
        public decimal Price { get; set; }
    }
}
