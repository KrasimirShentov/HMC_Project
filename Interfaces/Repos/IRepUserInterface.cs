using HMC_Project.Models;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepUserInterface
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> RegisterAsync(User user);
        Task<bool> ValidateUserAsync(string username, string password);
        Task DeleteUserAsync(Guid userId);

    }
}
