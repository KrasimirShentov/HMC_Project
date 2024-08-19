using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Versioning;
using System.Xml.Linq;

namespace HMC_Project.Services
{
    public class EmployeeServices : IEmployeeInterface
    {
        private readonly IRepEmployeeintefaces _employeeRepo;
        private readonly HMCDbContext _dbContext;

        public EmployeeServices(IRepEmployeeintefaces employeeRepo, HMCDbContext dbContext)
        {
            _employeeRepo = employeeRepo;
            _dbContext = dbContext;
        }

        public async Task<Employee> GetByIDAsync(Guid employeeId)
        {
            var result = await _employeeRepo.GetByIDAsync(employeeId);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepo.GetAllAsync();
        }

        public async Task<Employee> CreateAsync(EmployeeRequest employeeRequest)
        {
            var department = await _dbContext.Departments.FindAsync(employeeRequest.DepartmentId);
            var training = new Training("Permanent", "CEO", "Owner of a company", 168);

            var newEmployee = new Employee
            {
                ID = employeeRequest.ID,
                Name = employeeRequest.Name,
                Surname = employeeRequest.Surname,
                Age = employeeRequest.Age,
                Email = employeeRequest.Email,
                Position = employeeRequest.Position,
                Department = department,
                Training = training
            };

            _dbContext.Add(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            var existingEmployee = await _employeeRepo.GetByIDAsync(employee.ID);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.Age = employee.Age;
            existingEmployee.Email = employee.Email;
            existingEmployee.Position = employee.Position;
            existingEmployee.Gender = employee.Gender;

            _dbContext.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            var existingEmployee = await _employeeRepo.GetByIDAsync(employee.ID);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _dbContext.Remove(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
