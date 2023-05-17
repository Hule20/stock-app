using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalsProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {

        private readonly TestContext _testContext;

        public WatchListController(TestContext testContext)
        {
            _testContext = testContext;
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> AddStockToUser([FromBody] StockDTO stockDto, int id)
        {
            var userResult = await _testContext.Users.FindAsync(id);
            var stockResult = await _testContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == stockDto.Ticker);

            if (stockResult == null)
            {
                var newStock = new Stock
                {
                    Ticker = stockDto.Ticker,
                    Company = stockDto.Company
                };
                await _testContext.Stocks.AddAsync(newStock);
                await _testContext.SaveChangesAsync();

                stockResult = newStock;
            }

            WatchList newUserStock = new WatchList
            {
                UserID = userResult.ID,
                StockID = stockResult.ID
            };

            await _testContext.UserStocks.AddAsync(newUserStock);
            await _testContext.SaveChangesAsync();

            return Ok(userResult);
        }
    }
}
