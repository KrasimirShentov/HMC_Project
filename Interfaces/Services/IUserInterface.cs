using HMC_Project.Models;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<string> AuthenticateAsync(string username, string password);
        Task<User> RegisterUserAsync(User user);
        Task DeleteUserAsync(Guid userId);

    }
}
