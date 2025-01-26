using HMC_Project.Models;
using HMC_Project.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMC_Project.Interfaces.Services
{
    public interface ICompanyInterface
    {
        Task<Company> GetByIDAsync(Guid companyID);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> CreateAsync(CompanyRequest companyRequest);
        Task UpdateAsync(Guid companyID, CompanyRequest companyRequest);
        Task DeleteAsync(Guid companyID);
    }
}
