using FinalsProjectAPI.Data;
using FinalsProjectAPI.Features.Stocks.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalsProjectAPI.Features.Stocks.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly TestContext _testContext;

        public StockController(TestContext testContext)
        {
            _testContext = testContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stock>>> All()
        {
            var stocks = await _testContext.Stocks
                .ToListAsync();

            if (stocks == null)
            {
                return NotFound();
            }

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetById(int id)
        {
            var stock = await _testContext.Stocks
                .FirstOrDefaultAsync(i => i.ID == id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Stock newStock)
        {
            if (newStock == null)
            {
                return BadRequest();
            }

            if (newStock.Company == null)
            {
                return BadRequest("Company missing");
            }

            if (newStock.Ticker == null)
            {
                return BadRequest("Ticker missing");
            }

            var stock = new Stock
            {
                Ticker = newStock.Ticker,
                Company = newStock.Company
            };

            await _testContext.Stocks.AddAsync(stock);
            await _testContext.SaveChangesAsync();

            return Ok($"Stock ${stock.Ticker} created");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] Stock stockUpdate, int id)
        {
            var stock = await _testContext.FindAsync<Stock>(id);

            if (stock == null)
            {
                return NotFound();
            }

            if (stockUpdate.Company == null)
            {
                return BadRequest("Company missing");
            }

            if (stockUpdate.Ticker == null)
            {
                return BadRequest("Ticker missing");
            }

            stock.Ticker = stockUpdate.Ticker;
            stock.Company = stockUpdate.Company;

            _testContext.Entry(stock).State = EntityState.Modified;

            await _testContext.SaveChangesAsync();

            return Ok($"Stock ${stock.Ticker} updated");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartial([FromBody] Stock stockUpdate, int id)
        {
            var stock = await _testContext.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(stockUpdate.Ticker))
            {
                stock.Ticker = stockUpdate.Ticker;
            }

            if (!string.IsNullOrWhiteSpace(stockUpdate.Company))
            {
                stock.Company = stockUpdate.Company;
            }

            await _testContext.SaveChangesAsync();

            return Ok(stock);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var stock = await _testContext.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            _testContext.Stocks.Remove(stock);
            await _testContext.SaveChangesAsync();

            return Ok($"Succesfully deleted ${stock.Ticker} stock");
        }

    }
}
