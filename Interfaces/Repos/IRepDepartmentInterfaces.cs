using HMC_Project.Dtos;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepDepartmentInterfaces
    {
        Task<Department> GetByIDAsync(Guid DepartmentID);
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<Department> CreateAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);
    }
}
