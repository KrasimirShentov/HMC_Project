using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMC_Project.Services
{
    public class UserService : IUserService
    {
        private readonly IRepUserInterface _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IRepUserInterface userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);

            if (existingUser == null)
            {
                throw new ArgumentNullException(nameof(existingUser));
            }

            return existingUser;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var validUser = await _userRepository.ValidateUserAsync(username, password);
            if (!validUser)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User")
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<User> RegisterUserAsync(User user)
        {
            return await _userRepository.RegisterAsync(user);
        }
        public async Task DeleteUserAsync(Guid ID)
        {
            await _userRepository.DeleteUserAsync(ID);
        }
    }
}
