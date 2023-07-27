using XmTestTask.Core.Entities;

namespace XmTestTask.Core.Interfaces
{
    public interface IBTCPriceService
    {
        Task<BTCPrice> GetBTCPrice(int date, CancellationToken cancelationToken = default);

        Task<List<BTCPrice>> GetBTCPricesByDateRange(int startDate, int endDate, CancellationToken cancelationToken = default);
    }
}
