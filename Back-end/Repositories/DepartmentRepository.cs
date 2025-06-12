using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace HMC_Project.Repositories
{
    public class DepartmentRepository : IRepDepartmentInterfaces
    {
        private readonly HMCDbContext _dbContext;
        public DepartmentRepository(HMCDbContext hMCDbContext)
        {
            _dbContext = hMCDbContext;
        }
        public async Task<DepartmentDTO> GetByIDAsync(Guid DepartmentID)
        {
            var departmentEntity = await _dbContext.Departments
                .Include(d => d.Company)
                .Include(d => d.DepartmentAddresses)
                    .ThenInclude(da => da.Address)
                .Include(d => d.employees) // This line should load employees
                    .ThenInclude(e => e.Training)
                .Where(d => d.Id == DepartmentID)
                .FirstOrDefaultAsync(); // Execute the query and get the entity here

            // <--- PUT A BREAKPOINT ON THIS LINE

            if (departmentEntity == null)
            {
                return null; // Or throw an appropriate exception
            }

            // Now, inspect 'departmentEntity.employees' in your debugger.
            // Does it contain the employee you expect? Is it empty?

            return new DepartmentDTO
            {
                ID = departmentEntity.Id,
                Name = departmentEntity.Name,
                // ... map other properties from departmentEntity
                Addresses = departmentEntity.DepartmentAddresses.Select(da => new AddressDTO
                {
                    AddressName = da.Address.AddressName
                }).ToList(),
                Employees = departmentEntity.employees?.Select(e => new EmployeeDTO // Use null conditional operator here too
                {
                    Id = e.ID,
                    Name = e.Name,
                    Surname = e.Surname,
                    Age = e.Age,
                    Email = e.Email,
                    Position = e.Position,
                    Gender = e.Gender,
                    TrainingDTO = e.Training != null ? new TrainingDTO
                    {
                        Id = e.Training.ID,
                        Type = e.Training.Type,
                        PositionName = e.Training.PositionName,
                        Description = e.Training.Description,
                        TrainingHours = e.Training.TrainingHours
                    } : null
                }).ToList()
            };
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            return await _dbContext.Departments
            .Include(d => d.Company)
            .Include(d => d.DepartmentAddresses)
                .ThenInclude(da => da.Address)
            .Include(d => d.employees)
            .Select(d => new DepartmentDTO
            {
                ID = d.Id,
                Name = d.Name,
                Email = d.Email,
                Type = d.Type,
                PhoneNumber = d.PhoneNumber,
                Description = d.Description,
                CompanyID = d.Company.ID,
                CompanyName = d.Company.Name,
                CompanyDescription = d.Company.Description,
                Addresses = d.DepartmentAddresses.Select(da => new AddressDTO
                {
                    AddressName = da.Address.AddressName
                }).ToList(),
                Employees = d.employees.Select(e => new EmployeeDTO // Map employees for GetAll, but keep it light
                {
                    Id = e.ID,
                    Name = e.Name, // Or just Id for count
                }).ToList()
            })
            .ToListAsync();
        }
        public async Task<Department> CreateAsync(Department department)
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