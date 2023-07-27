using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using XmTestTask.API.Filters;
using XmTestTask.API.ViewModels;
using XmTestTask.Core.Helpers;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BTCPriceController : ControllerBase
    {
        private readonly IBTCPriceService btcPricecService;

        public BTCPriceController(IBTCPriceService btcPricecService)
        {
            this.btcPricecService = btcPricecService;
        }

        /// <summary>
        /// Retrives BTC price by date 
        /// </summary>
        /// <param name="date">Unix date. It should have hour accuracy.</param>
        [HttpGet("{date}")]
        [Tags("Get BTC price")]
        [ValidDateParam("date")]
        public async Task<IActionResult> Get([BindRequired] long date, CancellationToken cancellationToken)
        {
            var dateIntFormat = DateHelper.ConvertUnixDateToInt(date);
            var btcPrice = await btcPricecService.GetBTCPrice(dateIntFormat, cancellationToken);
            var result = BTCPriceViewModel.FromBTCPrice(btcPrice);
            return Ok(result);
        }

        /// <summary>
        /// Retrives BTC prices within date range
        /// </summary>
        /// <param name="startDate">Start date of range. It should be in unix format with hour accuracy.</param>
        /// <param name="endDate">End date of range. It should be in unix format with hour accuracy.</param>
        [HttpGet()]
        [Tags("Get BTC prices in range")]
        [ValidDateParam("startDate")]
        [ValidDateParam("endDate")]
        public async Task<IActionResult> GetList([BindRequired, FromQuery] long startDate, [BindRequired, FromQuery] long endDate, CancellationToken cancellationToken)
        {
            var startDateIntFormat = DateHelper.ConvertUnixDateToInt(startDate);
            var endDateIntFormat = DateHelper.ConvertUnixDateToInt(endDate);
            var btcPrices = await btcPricecService.GetBTCPricesByDateRange(DateHelper.ConvertUnixDateToInt(startDate), DateHelper.ConvertUnixDateToInt(endDate), cancellationToken);
            var result = btcPrices.Select(c => BTCPriceViewModel.FromBTCPrice(c));
            return Ok(result);
        }
    }
}