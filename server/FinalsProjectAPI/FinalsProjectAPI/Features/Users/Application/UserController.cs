using FinalsProjectAPI.Data;
using FinalsProjectAPI.Features.Users.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalsProjectAPI.Features.Users.Application
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly TestContext _testContext;

        public UserController(TestContext testContext)
        {
            _testContext = testContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> All()
        {
            var users = await _testContext.Users
                .Include(u => u.Watchlist)
                .ThenInclude(s => s.Stock)
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _testContext.Users
                .Include(w => w.Watchlist)
                .ThenInclude(s => s.Stock)
                .FirstOrDefaultAsync(i => i.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User newUser)
        {
            await _testContext.Users.AddAsync(newUser);
            await _testContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromBody] User userUpdate, int id)
        {
            var user = await _testContext.FindAsync<User>(id);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;
            user.Email = userUpdate.Email;
            user.Password = userUpdate.Password;

            _testContext.Entry(user).State = EntityState.Modified;

            await _testContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartial(UserPartialUpdateDTO newUser, int id)
        {
            var user = await _testContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(newUser.FirstName))
            {
                user.FirstName = newUser.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(newUser.LastName))
            {
                user.LastName = newUser.LastName;
            }

            if (!string.IsNullOrWhiteSpace(newUser.Email))
            {
                user.Email = newUser.Email;
            }

            if (!string.IsNullOrWhiteSpace(newUser.Password))
            {
                user.Password = newUser.Password;
            }

            await _testContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _testContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _testContext.Users.Remove(user);
            await _testContext.SaveChangesAsync();

            return Ok($"user {user.FirstName} {user.LastName} successfully deleted");
        }
    }
}
