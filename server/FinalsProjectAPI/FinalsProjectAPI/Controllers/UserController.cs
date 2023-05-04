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
                .Include(u => u.UserStocks)
                .ThenInclude(s => s.Stock)
                .ToListAsync();

            var mappedUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                //await Console.Out.WriteLineAsync(user);
                mappedUsers.Add(UserDTO.MapFrom(user));
            }

            return Ok(mappedUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Single(int id)
        {
            var user = await _testContext.Users
                .Include(u => u.UserStocks)
                .ThenInclude(s => s.Stock)
                .FirstOrDefaultAsync(i => i.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            var mappedUser = UserDTO.MapFrom(user);


            return Ok(mappedUser);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> Create([FromBody] UserDTO userDto)
        {
            var user = new User
            {
                //ID = userDto.ID, nepotrebna linija jer sam dodjeljuje vrijednost ID-a
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
            };
            await _testContext.Users.AddAsync(user);
            await _testContext.SaveChangesAsync();

            return Ok(user);
        }

        //ccording to the HTTP specification, a PUT request requires the client to send
        //the entire updated entity, not just the changes. To support partial updates, use HTTP PATCH.
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromBody] User userDto, int id)
        {
            if (id != userDto.ID)
            {
                return NotFound();
            }

            _testContext.Entry(userDto).State = EntityState.Modified;

            await _testContext.SaveChangesAsync();


            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> UpdatePartial([FromBody] User userDto, int id)
        {
            var result = await _testContext.Users.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(userDto.FirstName))
            {
                result.FirstName = userDto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userDto.LastName))
            {
                result.LastName = userDto.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                result.Email = userDto.Email;
            }

            await _testContext.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var result = await _testContext.Users.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _testContext.Users.Remove(result);
            await _testContext.SaveChangesAsync();

            return Ok(result);
        }

    }
}
