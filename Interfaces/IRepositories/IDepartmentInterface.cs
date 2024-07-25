using HMC_Project.Models;

namespace HMC_Project.Interfaces.IRepositories
{
    public interface IDepartmentInterface
    {
        Task<Department> GetByIDAsync(int DepartmentID);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> CreateAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);

    }
}
