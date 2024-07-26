using HMC_Project.Interfaces.IRepositories;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Services
{
    public class DepartmentServices : IDepartmentInterface
    {
        private readonly IDepartmentInterface _department;
        private HMCDbContext _dbContext;

        public DepartmentServices(IDepartmentInterface departmentInterface)
        {
            _department = departmentInterface;
        }
        public async Task<Department> GetByIDAsync(Guid DepartmentID)
        {
            var result = await _department.GetByIDAsync(DepartmentID);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _department.GetAllAsync();
        }
        public async Task<Department> CreateAsync(DepartmentRequest departmentRequest)
        {
            var result = await _department.GetByIDAsync(departmentRequest.ID);

            if (result != null)
            {
                throw new ArgumentException("A department with this ID already exists.");
            }

            var newDepartment = MapRequestToDepartment(departmentRequest);
            _dbContext.Add(newDepartment);
            await _dbContext.SaveChangesAsync();

            return newDepartment;
        }
        public async Task UpdateAsync(Department department)
        {
            var ExistingDeprt = await _department.GetByIDAsync(department.Id);
            if (ExistingDeprt == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            ExistingDeprt.Name = department.Name;
            ExistingDeprt.Description = department.Description;
            ExistingDeprt.Type = department.Type;
            ExistingDeprt.Email = department.Email;
            ExistingDeprt.PhoneNumber = department.PhoneNumber;

            _dbContext.Update(ExistingDeprt);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Department department)
        {
            var deprt = await _department.GetByIDAsync(department.Id);

            if (deprt == null)
            {
                throw new ArgumentNullException(nameof(deprt));
            }

            _dbContext.Remove(deprt);
            await _dbContext.SaveChangesAsync();
        }

        private Department MapRequestToDepartment(DepartmentRequest departmentRequest)
        {
            return new Department
            {
                Id = departmentRequest.ID != Guid.Empty ? departmentRequest.ID : Guid.NewGuid(),
                Name = departmentRequest.Name,
                Description = departmentRequest.Description,
                Type = departmentRequest.Type,
                Email = departmentRequest.Email,
                PhoneNumber = departmentRequest.PhoneNumber
            };
        }
    }
}
