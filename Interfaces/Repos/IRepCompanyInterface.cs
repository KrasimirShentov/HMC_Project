using HMC_Project.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepCompanyInterface
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIDAsync(Guid companyID);
        Task<Company> CreateAsync(Company company);
        Task UpdateAsync(Guid companyID, Company company);
        Task DeleteAsync(Guid companyID);
    }
}
