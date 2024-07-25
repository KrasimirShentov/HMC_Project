using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;

namespace HMC_Project.Repositories
{
    public class EmployeeReposity : IEmployeeInterface
    {
        public Task<Employee> GetByIDAsync(int EmployeeID)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Employee> CreateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
