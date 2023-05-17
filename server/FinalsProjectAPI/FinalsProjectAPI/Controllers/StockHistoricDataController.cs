using FinalsProjectAPI.Clients;
using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Controllers
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
