using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface IUserInterface
    {
        Task<User> GetUserAsync(string username);
        Task<User> GetByIDAsync(Guid UserID);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(UserRequest userRequest);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
