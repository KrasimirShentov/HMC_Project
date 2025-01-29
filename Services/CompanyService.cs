using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HMC_Project.Services
{
    public class CompanyService : ICompanyInterface
    {
        private readonly IRepCompanyInterface _companyRepository;
        private readonly HMCDbContext _dbContext;

        public CompanyService(IRepCompanyInterface companyRepository, HMCDbContext dbContext)
        {
            _companyRepository = companyRepository;
            _dbContext = dbContext;
        }

        public async Task<CompanyDTO> GetByIDAsync(Guid companyID)
        {
            return await _companyRepository.GetByIDAsync(companyID);
        }
        public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
        {
            return await _companyRepository.GetAllAsync();
        }
        public async Task<Company> CreateAsync(CompanyRequest companyRequest)
        {
            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Name == companyRequest.Name);
            if (existingCompany != null)
            {
                throw new ArgumentException("Company with the same name already exists.");
            }

            var newCompany = new Company
            {
                ID = Guid.NewGuid(),
                Name = companyRequest.Name,
                Description = companyRequest.Description,
            };

            foreach (var addressDto in companyRequest.Addresses)
            {
                // Check if the address already exists in the database
                var existingAddress = await _dbContext.Addresses
                    .FirstOrDefaultAsync(a => a.AddressName == addressDto.AddressName);

                if (existingAddress == null)
                {
                    var newAddress = new Address
                    {
                        AddressName = addressDto.AddressName,
                        Company = newCompany
                    };
                    newCompany.Addresses.Add(newAddress);
                }
                else
                {
                    newCompany.Addresses.Add(existingAddress);
                }
            }

            await _companyRepository.CreateAsync(newCompany);
            await _dbContext.SaveChangesAsync();

            return newCompany;
        }


        public async Task UpdateAsync(Guid companyID, CompanyRequest companyRequest)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ID == companyID);
            if (company == null)
            {
                throw new KeyNotFoundException("Company not found");
            }

            company.Name = companyRequest.Name;
            company.Description = companyRequest.Description;

            _dbContext.Update(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            var delCompany = await _dbContext.Companies.FindAsync(ID);

            if (delCompany == null)
            {
                throw new ArgumentNullException(nameof(delCompany));
            }

            _dbContext.Companies.Remove(delCompany);
            await _dbContext.SaveChangesAsync();
        }
    }
}
