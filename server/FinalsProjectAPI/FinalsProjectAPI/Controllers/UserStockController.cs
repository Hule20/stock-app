using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStockController : ControllerBase
    {

        private readonly TestContext _testContext;

        public UserStockController(TestContext testContext)
        {
            _testContext = testContext;
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> AddStockToUser([FromBody] int stockId, int id)
        {
            var userResult = await _testContext.Users.FindAsync(id);
            var stockResult = await _testContext.Stocks.FindAsync(stockId);

            UserStock newUserStock = new UserStock
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
