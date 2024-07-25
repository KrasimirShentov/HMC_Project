using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;

namespace HMC_Project.Services
{
    public class DepartmentServices : IDepartmentInterface
    {
        public Task<Department> GetByIDAsync(int DepartmentID)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Department> CreateAsync(Department department)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Department department)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
