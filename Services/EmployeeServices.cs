using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Services
{
    public class EmployeeServices : IEmployeeInterface
    {
        private readonly IEmployeeInterface _employeeRepository;
        private readonly HMCDbContext _dbContext;

        public EmployeeServices(IEmployeeInterface employeeRepository, HMCDbContext dbContext)
        {
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
        }

        public async Task<Employee> GetByIDAsync(Guid employeeId)
        {
            var result = await _employeeRepository.GetByIDAsync(employeeId);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> CreateAsync(EmployeeRequest employeeRequest)
        {
            var result = await _employeeRepository.GetByIDAsync(employeeRequest.ID);

            if (result != null)
            {
                throw new ArgumentException("An employee with this ID already exists.");
            }

            var newEmployee = MapRequestToEmployee(employeeRequest);
            _dbContext.Add(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIDAsync(employee.ID);
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
            existingEmployee.Training = employee.Training;
            existingEmployee.Department = employee.Department;
            existingEmployee.Birthday = employee.Birthday;
            existingEmployee.HireDate = employee.HireDate;

            _dbContext.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIDAsync(employee.ID);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            _dbContext.Remove(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }

        private Employee MapRequestToEmployee(EmployeeRequest employeeRequest)
        {
            return new Employee(
                employeeRequest.ID != Guid.Empty ? employeeRequest.ID : Guid.NewGuid(),
                employeeRequest.Name,
                employeeRequest.Surname,
                employeeRequest.Age,
                employeeRequest.Email,
                employeeRequest.Position,
                employeeRequest.Training,
                employeeRequest.Department)
            {
                Gender = employeeRequest.Gender
            };
        }
    }
}
