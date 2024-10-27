using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;

namespace HMC_Project.Repositories
{
    public class EmployeeReposity : IRepEmployeeintefaces
    {
        private readonly HMCDbContext _dbContext;
        public EmployeeReposity(HMCDbContext hMCDbContext)
        {
            _dbContext = hMCDbContext;
        }
        public async Task<Employee> GetByIDAsync(Guid EmployeeID)
        {
            return await _dbContext.Employees.FindAsync(EmployeeID);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.Training)  
                .Include(e => e.Department) 
                .ToListAsync();
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
