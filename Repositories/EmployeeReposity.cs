using HMC_Project.Dtos;
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
        public async Task<EmployeeDTO> GetByIDAsync(Guid EmployeeID)
        {
            return await _dbContext.Employees
        .Include(e => e.Department)
        .Include(e => e.Training)
        .Where(e => e.ID == EmployeeID)
        .Select(e => new EmployeeDTO
        {
            Id = e.ID,
            Name = e.Name,
            Surname = e.Surname,
            Age = e.Age,
            Email = e.Email,
            Position = e.Position,
            Gender = e.Gender,
            DepartmentDTO = new DepartmentDTO
            {
                ID = e.Department.Id,
                Name = e.Department.Name,
                Email = e.Department.Email,
                Type = e.Department.Type,
                PhoneNumber = e.Department.PhoneNumber,
                Description = e.Department.Description
            },
            TrainingDTO = new TrainingDTO
            {
                Id = e.Training.ID,
                Type = e.Training.Type,
                PositionName = e.Training.PositionName,
                Description = e.Training.Description,
                TrainingHours = e.Training.TrainingHours
            }
        })
        .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.Training)
                .Include(e => e.Department)
                .Select(e => new EmployeeDTO
                {
                    Id = e.ID,
                    Name = e.Name,
                    Surname = e.Surname,
                    Age = e.Age,
                    Email = e.Email,
                    Position = e.Position,
                    DepartmentDTO = e.Department == null ? null : new DepartmentDTO
                    {
                        ID = e.Department.Id,
                        Name = e.Department.Name,
                        Email = e.Department.Email,
                        Type = e.Department.Type
                    },
                    TrainingDTO = e.Training == null ? null : new TrainingDTO
                    {
                        Id = e.Training.ID,
                        Type = e.Training.Type,
                        Description = e.Training.Description,
                        TrainingHours = e.Training.TrainingHours
                    }
                })
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
