using System.Collections.Concurrent;
using System.Threading.Tasks;
using XmTestTask.Core.Entities;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Core.Services
{
    public class BTCPriceService : IBTCPriceService
    {
        private readonly IBTCRepository repository;
        private readonly IBTCPriceCalculationService calculationService;
        private readonly IEnumerable<IBTCPriceDownloadService> downloadServices;

        public BTCPriceService(IBTCRepository repository,
            IBTCPriceCalculationService calculationService,
            IEnumerable<IBTCPriceDownloadService> downloadServices)
        {
            this.repository = repository;
            this.calculationService = calculationService;
            this.downloadServices = downloadServices;
        }

        public async Task<BTCPrice> GetBTCPrice(int date, CancellationToken cancelationToken = default)
        {
            var price = await repository.GetByIdAsync(date, cancelationToken);
            if (price != null)
                return price;

            var newPrices = new ConcurrentBag<BTCPrice>();

            await Parallel.ForEachAsync(downloadServices, cancelationToken, async (service, token) =>
            {
                newPrices.Add(await service.GetBTCPrice(date, token));
            }); 

            var newPrice = new BTCPrice() { Price = calculationService.Calculate(newPrices), Date = date };

            await repository.CreateOrUpdateAsync(newPrice, cancelationToken);

            return newPrice;
        }

        public async Task<List<BTCPrice>> GetBTCPricesByDateRange(int startDate, int endDate, CancellationToken cancelationToken = default)
        {
            return await repository.GetByDateRangeAsync(startDate, endDate, cancelationToken);
        }
    }
}
