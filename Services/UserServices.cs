using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;

namespace HMC_Project.Services
{
    public class UserServices : IUserInterface
    {
        public Task<User> GetByIDAsync(int UserID)
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
        public Task<User> CreateAsync(User user)
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
