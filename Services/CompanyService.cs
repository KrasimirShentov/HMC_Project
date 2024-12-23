﻿using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
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

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company> GetByIDAsync(Guid companyID)
        {
            return await _companyRepository.GetByIDAsync(companyID);
        }

        public async Task<Company> CreateAsync(CompanyRequest companyRequest)
        {
            //var dep = await _dbContext.Companies.FindAsync(companyRequest)

            var company = new Company
            {
                ID = Guid.NewGuid(),
                Name = companyRequest.Name,
                Description = companyRequest.Description,
            };

            return await _companyRepository.CreateAsync(company);
        }

        public async Task UpdateAsync(Guid companyID, CompanyRequest companyRequest)
        {
            var company = await _companyRepository.GetByIDAsync(companyID);
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
