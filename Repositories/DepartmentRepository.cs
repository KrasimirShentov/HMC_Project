using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
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
        public async Task<DepartmentDTO> GetByIDAsync(Guid DepartmentID)
        {
            var department = await _dbContext.Departments
        .Include(d => d.Company)
        .FirstOrDefaultAsync(d => d.Id == DepartmentID);

            if (department == null)
            {
                return null;
            }

            return new DepartmentDTO
            {
                ID = department.Id,
                Name = department.Name,
                Email = department.Email,
                Type = department.Type,
                PhoneNumber = department.PhoneNumber,
                Description = department.Description,
                CompanyID = department.Company.ID,
                CompanyName = department.Company.Name,
                CompanyDescription = department.Company.Description
            };
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            return await _dbContext.Departments
            .Include(d => d.Company)
            .Include(d => d.DepartmentAddresses)
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
                DTOAddresses = d.DepartmentAddresses.Select(da => new AddressDTO
                {
                    AddressName = da.Address.AddressName
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
