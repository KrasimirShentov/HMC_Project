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

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.Include(c => c.Departments).Include(c => c.Addresses).ToListAsync();
        }

        public async Task<Company> GetByIDAsync(Guid companyID)
        {
            return await _dbContext.Companies
                                   .Include(c => c.Departments)
                                   .Include(c => c.Addresses)
                                   .SingleOrDefaultAsync(c => c.ID == companyID);
        }

        public async Task<Company> CreateAsync(Company company)
        {
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task UpdateAsync(Guid companyID, Company company)
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid companyID)
        {
            var company = await GetByIDAsync(companyID);
            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
