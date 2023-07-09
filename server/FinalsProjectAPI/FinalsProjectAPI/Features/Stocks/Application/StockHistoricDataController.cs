using FinalsProjectAPI.Features.News.Infrastructure;
using FinalsProjectAPI.Features.Stocks.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Features.Stocks.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockHistoricDataController : ControllerBase
    {
        private readonly IAlphaVantageClient _alphaVantageClient;

        public StockHistoricDataController(IAlphaVantageClient alphaVantageClient)
        {
            _alphaVantageClient = alphaVantageClient;
        }


        [HttpGet("stock/{ticker}")]
        public async Task<ActionResult<List<StockHistoricPrice>>> GetData(string ticker)
        {
            var stockData = await _alphaVantageClient.GetStockHistoricPrice(ticker);

            return stockData is not null ? Ok(stockData) : NotFound();
        }
    }
}
