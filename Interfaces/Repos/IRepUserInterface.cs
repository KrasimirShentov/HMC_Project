using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepUserInterface
    {
        Task<User> GetUserAsync(string username);
        Task<User> GetByIDAsync(Guid UserID);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
