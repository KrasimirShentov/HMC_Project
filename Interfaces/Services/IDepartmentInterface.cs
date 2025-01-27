using HMC_Project.Dtos;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface IDepartmentInterface
    {
        Task<Department> GetByIDAsync(Guid DepartmentID);
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<Department> CreateAsync(DepartmentRequest departmentRequest);
        Task UpdateAsync(Guid ID,DepartmentRequest departmentRequest);
        Task DeleteAsync(Guid ID);

    }
}
