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

            List<BTCPrice> newPrices = new List<BTCPrice>();
            foreach (var service in downloadServices)
            {
                newPrices.Add(await service.GetBTCPrice(date, cancelationToken));
            }

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
