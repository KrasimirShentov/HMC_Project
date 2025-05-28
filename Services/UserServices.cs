// HMC_Project.Services/UserService.cs
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Dtos; // Add this using directive for UserRegistraionDTO and AddressDTO
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net; // Ensure this is installed via NuGet and used for hashing
using System.Collections.Generic; // Needed for List<Address>

namespace HMC_Project.Services
{
    public class UserService : IUserService
    {
        private readonly IRepUserInterface _userRepository;
        private readonly IConfiguration _configuration;
        // private readonly HMCDbContext _dbContext; // Removed from previous suggestions, ensure it's gone if you followed

        public UserService(IRepUserInterface userRepository, IConfiguration configuration /*, HMCDbContext dbContext */)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            // _dbContext = dbContext; // Ensure this is commented out or removed
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);

            if (existingUser == null)
            {
                throw new ArgumentNullException(nameof(existingUser), "User not found.");
            }

            return existingUser;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Invalid credentials
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
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

        // This method is used internally by RegisterUserWithAddressesAsync to hash the password
        // and pass the User model to the repository.
        public async Task<User> RegisterUserAsync(User user)
        {
            // Password should already be hashed by RegisterUserWithAddressesAsync
            return await _userRepository.RegisterAsync(user);
        }

        // NEW METHOD: Handles DTO to Model conversion and password hashing
        public async Task<User> RegisterUserWithAddressesAsync(UserRegistraionDTO userDto)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetByUsernameAsync(userDto.UserName);
            if (existingUser != null)
            {
                throw new ArgumentException("Username already exists.");
            }

            // Create User model from DTO
            var newUser = new User
            {
                ID = Guid.NewGuid(),
                Name = userDto.Name,
                Surname = userDto.Surname,
                UserName = userDto.UserName,
                // Hash the password here before assigning to the model
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Email = userDto.Email,
                Gender = userDto.Gender,
                DateOfBirth = userDto.DateOfBirth,
                Addresses = new List<Address>() // Initialize collection
            };

            // Handle addresses from DTO and associate with the new user
            if (userDto.Addresses != null)
            {
                foreach (var addressDto in userDto.Addresses)
                {
                    // Assuming Address has a default constructor and UserId property
                    var newAddress = new Address
                    {
                        AddressName = addressDto.AddressName,
                        UserID = newUser.ID // Link address to the new user
                    };
                    newUser.Addresses.Add(newAddress);
                }
            }

            // Pass the fully constructed User model (with hashed password and addresses) to the repository
            return await _userRepository.RegisterAsync(newUser);
        }

        public async Task DeleteUserAsync(Guid ID)
        {
            await _userRepository.DeleteUserAsync(ID);
        }
    }
}