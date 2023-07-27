using XmTestTask.Core.Entities;

namespace XmTestTask.Core.Interfaces
{
    public interface IBTCPriceDownloadService
    {
        public Task<BTCPrice> GetBTCPrice(int date, CancellationToken cancelationToken = default);
    }
}
