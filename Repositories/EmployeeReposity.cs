using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Repositories
{
    public class EmployeeReposity : IEmployeeInterface
    {
        public Task<Employee> GetByIDAsync(Guid EmployeeID)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Employee> CreateAsync(EmployeeRequest employee)
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
