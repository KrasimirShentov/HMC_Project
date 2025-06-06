﻿using HMC_Project.Interfaces.Repos;
using HMC_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HMC_Project.Repositories
{
    public class UserRepository : IRepUserInterface
    {
        private readonly HMCDbContext _dbContext;

        public UserRepository(HMCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> RegisterAsync(User user)
        {

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user == null || user.Password != password)
            {
                return false;
            }
            return true;
        }
        public async Task DeleteUserAsync(Guid ID)
        {
            var user = await _dbContext.Users.FindAsync(ID);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
