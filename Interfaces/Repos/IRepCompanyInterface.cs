using HMC_Project.Dtos;
using HMC_Project.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepCompanyInterface
    {
        Task<Company> GetByIDAsync(Guid companyID);
        Task<IEnumerable<CompanyDTO>> GetAllAsync();
        Task<Company> CreateAsync(Company company);
        Task UpdateAsync(Guid companyID, Company company);
        Task DeleteAsync(Guid companyID);
    }
}
