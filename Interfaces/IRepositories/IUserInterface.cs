using HMC_Project.Models;

namespace HMC_Project.Interfaces.IRepositories
{
    public interface IUserInterface
    {
        Task<User> GetUserAsync(string username);
        Task<User> GetByIDAsync(int UserID);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);


    }
}
