﻿using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface IEmployeeInterface
    {
        Task<Employee> GetByIDAsync(Guid EmployeeID);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateAsync(EmployeeRequest employeeRequest);
        Task UpdateAsync(Guid ID,EmployeeRequest employeeRequest);
        Task DeleteAsync(Guid ID);
    }
}
