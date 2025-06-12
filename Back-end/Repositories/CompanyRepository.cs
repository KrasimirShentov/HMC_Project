using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMC_Project.Repositories
{
    public class CompanyRepository : IRepCompanyInterface
    {
        private readonly HMCDbContext _dbContext;

        public CompanyRepository(HMCDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CompanyDTO> GetByIDAsync(Guid companyID)
        {
            return await _dbContext.Companies
        .Include(c => c.Departments)
        .Where(c => c.ID == companyID)
        .Select(c => new CompanyDTO
        {
            Id = c.ID,
            Name = c.Name,
            Description = c.Description,
            Departments = c.Departments.Select(d => new DepartmentDTO
            {
                ID = d.Id,
                Name = d.Name,
                Description = d.Description
            }).ToList(),
            Addresses = c.Addresses
            .Select(ca => new AddressDTO
            {
                AddressName = ca.AddressName
            })
            .ToList()
        }).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
        {
            return await _dbContext.Companies
                .Include(c => c.Departments)
                .Include(c => c.Addresses)
                .Select(c => new CompanyDTO
                {
                    Id = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    Departments = c.Departments
                        .Select(d => new DepartmentDTO
                        {
                            ID = d.Id,
                            Name = d.Name,
                            Description = d.Description,
                            Type = d.Type,
                            Email = d.Email,
                            PhoneNumber = d.PhoneNumber
                        }).ToList(),
                    Addresses = c.Addresses
                        .Select(a => new AddressDTO
                        {
                            AddressName = a.AddressName
                        }).ToList()
                })
                .ToListAsync();
        }




        public async Task<Company> CreateAsync(Company company)
        {
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task UpdateAsync(Company company)
        {
            _dbContext.Entry(company).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid companyID)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ID == companyID);
            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
