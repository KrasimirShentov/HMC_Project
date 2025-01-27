using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepEmployeeintefaces
    {
        Task<Employee> GetByIDAsync(Guid EmployeeID);
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
