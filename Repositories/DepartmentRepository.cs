using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Repositories
{
    public class DepartmentRepository : IDepartmentInterface
    {
        public Task<Department> GetByIDAsync(Guid DepartmentID)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Department> CreateAsync (DepartmentRequest departmentRequest)
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
