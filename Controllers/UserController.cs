using HMC_Project.Dtos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly HMCDbContext _dbContext;

        public UserController(IUserService userService, HMCDbContext _dbcontext)
        {
            _userService = userService;
            _dbContext = _dbcontext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var token = await _userService.AuthenticateAsync(userRequest.UserName, userRequest.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistraionDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                ID = Guid.NewGuid(),
                Name = userDto.Name,
                Surname = userDto.Surname,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                Gender = userDto.Gender,
                DateOfBirth = userDto.DateOfBirth
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            foreach (var addressDto in userDto.Addresses)
            {
                var address = new Address(addressDto.AddressName, user.ID);
                _dbContext.Addresses.Add(address);
            }

            _dbContext.SaveChanges();

            return Ok(new { user.ID, user.Name });
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
