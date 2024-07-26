using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Identity;

namespace HMC_Project.Repositories
{
    public class UserRepository : IUserInterface
    {
        public Task<User> GetByIDAsync(Guid UserID)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserAsync(string username)
        {
            throw new NotImplementedException();
        }
        public Task<User> CreateAsync(UserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
