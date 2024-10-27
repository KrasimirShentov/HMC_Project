using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HMC_Project.Repositories
{
    public class DepartmentRepository : IRepDepartmentInterfaces
    {
        private readonly HMCDbContext _dbContext;
        public DepartmentRepository(HMCDbContext hMCDbContext)
        {
            _dbContext = hMCDbContext;
        }
        public async Task<Department> GetByIDAsync(Guid DepartmentID)
        {
            return await _dbContext.Departments.FindAsync(DepartmentID);
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        public async Task<Department> CreateAsync (Department department)
        {
            _dbContext.Add(department);
            await _dbContext.SaveChangesAsync();
            return department;
        }
        public async Task UpdateAsync(Department department)
        {
            _dbContext.Entry(department).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Department department)
        {
            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
        }
    }
}
