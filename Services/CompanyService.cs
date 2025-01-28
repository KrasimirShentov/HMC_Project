using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            var company = new Company
            {
                ID = Guid.NewGuid(),
                Name = companyRequest.Name,
                Description = companyRequest.Description,
            };

            //foreach (var addressRequest in companyRequest.C_Addresses)
            //{
            //    var address = new Address
            //    {
            //        AddressName = addressRequest.AddressName,
            //        CompanyID = company.ID 
            //    };
            //    _dbContext.Addresses.Add(address);
            //}

            await _companyRepository.CreateAsync(company);
            await _dbContext.SaveChangesAsync();

            return company;
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
            await _companyRepository.UpdateAsync(companyID, company);
        }

        public async Task DeleteAsync(Guid companyID)
        {
            await _companyRepository.DeleteAsync(companyID);
        }
    }
}
