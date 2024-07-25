using HMC_Project.Models;

namespace HMC_Project.Interfaces.IRepositories
{
    public interface IEmployeeInterface
    {
        Task<Employee> GetByIDAsync(int EmployeeID);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
