using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalsProjectAPI.Controllers
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

            var mappedStocks = new List<StockDTO>();
            foreach (var stock in stocks)
            {
                mappedStocks.Add(StockDTO.MapFrom(stock));
            }

            return Ok(mappedStocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> Single(int id)
        {
            var stock = await _testContext.Stocks
                .FirstOrDefaultAsync(i => i.ID == id);

            if (stock == null)
            {
                return NotFound();
            }

            var mappedStock = StockDTO.MapFrom(stock);


            return Ok(mappedStock);
        }

        [HttpPost]
        public async Task<ActionResult<List<Stock>>> Create([FromBody] Stock stockDto)
        {
            var stock = new Stock
            {
                //ID = userDto.ID, nepotrebna linija jer sam dodjeljuje vrijednost ID-a
                Ticker = stockDto.Ticker,
                Company = stockDto.Company
            };
            await _testContext.Stocks.AddAsync(stock);
            await _testContext.SaveChangesAsync();

            return Ok(stock);
        }

        //ccording to the HTTP specification, a PUT request requires the client to send
        //the entire updated entity, not just the changes. To support partial updates, use HTTP PATCH.
        [HttpPut("{id}")]
        public async Task<ActionResult<Stock>> Update([FromBody] Stock stockDto, int id)
        {
            if (id != stockDto.ID)
            {
                return NotFound();
            }

            _testContext.Entry(stockDto).State = EntityState.Modified;

            await _testContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Stock>> UpdatePartial([FromBody] Stock stockDto, int id)
        {
            var stock = await _testContext.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(stockDto.Ticker))
            {
                stock.Ticker = stockDto.Ticker;
            }

            if (!string.IsNullOrWhiteSpace(stockDto.Company))
            {
                stock.Company = stockDto.Company;
            }

            await _testContext.SaveChangesAsync();

            return Ok(stock);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Stock>> Delete(int id)
        {
            var stock = await _testContext.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            _testContext.Stocks.Remove(stock);
            await _testContext.SaveChangesAsync();

            return Ok(stock);
        }

    }
}
