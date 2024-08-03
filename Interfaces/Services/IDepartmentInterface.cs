using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface IDepartmentInterface
    {
        Task<Department> GetByIDAsync(Guid DepartmentID);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> CreateAsync(DepartmentRequest departmentRequest);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);

    }
}
