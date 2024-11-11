using HMC_Project.Models;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<User> RegisterUserAsync(User user);
    }
}
