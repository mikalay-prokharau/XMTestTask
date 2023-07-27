using XmTestTask.Core.Entities;

namespace XmTestTask.Core.Interfaces
{
    public interface IBTCPriceCalculationService
    {
        decimal Calculate(IEnumerable<BTCPrice> prices);
    }
}
