using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
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

        public async Task<EmployeeDTO> GetByIDAsync(Guid employeeId)
        {
            var result = await _employeeRepo.GetByIDAsync(employeeId);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            return await _employeeRepo.GetAllAsync();
        }

        public async Task<Employee> CreateAsync(EmployeeRequest employeeRequest)
        {
            if (employeeRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeRequest), "Employee request data is missing.");
            }

            var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == employeeRequest.Email);
            if (existingEmployee != null)
            {
                throw new ArgumentException($"Employee with the email '{employeeRequest.Email}' already exists.");
            }

            var department = await _dbContext.Departments.FindAsync(employeeRequest.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException($"Invalid Department ID: '{employeeRequest.DepartmentId}'. Department not found.");
            }

            var training = await _dbContext.Training.FindAsync(employeeRequest.TrainingId);
            if (training == null)
            {
                throw new Exception("Training with given ID does not exist.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Found Training ID: {training.ID}");
            }

            var newEmployee = new Employee
            {
                ID = Guid.NewGuid(), 
                Name = employeeRequest.Name,
                Surname = employeeRequest.Surname,
                Age = employeeRequest.Age,
                Email = employeeRequest.Email,
                Position = employeeRequest.Position,
                Gender = employeeRequest.Gender,
                Department = department,
                TrainingID = employeeRequest.TrainingId
            };

            _dbContext.Employees.Add(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task UpdateAsync(Guid ID, EmployeeRequest employeeRequest)
        {
            var existingEmployee = await _dbContext.Employees.FindAsync(ID);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            existingEmployee.Name = employeeRequest.Name;
            existingEmployee.Surname = employeeRequest.Surname;
            existingEmployee.Age = employeeRequest.Age;
            existingEmployee.Email = employeeRequest.Email;
            existingEmployee.Position = employeeRequest.Position;
            existingEmployee.Gender = employeeRequest.Gender;

            _dbContext.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            var existingEmployee = await _dbContext.Employees.FindAsync(ID);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _dbContext.Remove(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
