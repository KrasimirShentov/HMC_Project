using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Services
{
    public class DepartmentServices : IDepartmentInterface
    {
        private readonly IRepDepartmentInterfaces _departmentRepo;
        private HMCDbContext _dbContext;

        public DepartmentServices(IRepDepartmentInterfaces departmentRepo, HMCDbContext hMCDbContext)
        {
            _departmentRepo = departmentRepo;
            _dbContext = hMCDbContext;
        }
        public async Task<Department> GetByIDAsync(Guid DepartmentID)
        {
            var result = await _departmentRepo.GetByIDAsync(DepartmentID);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepo.GetAllAsync();
        }
        public async Task<Department> CreateAsync(DepartmentRequest departmentRequest)
        {
            var newDepartment = new Department
            {
                Id = departmentRequest.ID,
                Name = departmentRequest.Name,
                Description = departmentRequest.Description,
                Type = departmentRequest.Type,
                Email = departmentRequest.Email,
                PhoneNumber = departmentRequest.PhoneNumber
            };

            _dbContext.Add(newDepartment);
            await _dbContext.SaveChangesAsync();

            return newDepartment;
        }
        public async Task UpdateAsync(Guid ID, DepartmentRequest departmentRequest)
        {
            var ExistingDeprt = await _departmentRepo.GetByIDAsync(ID);
            if (ExistingDeprt == null)
            {
                throw new ArgumentNullException("Department not found");
            }

            ExistingDeprt.Name = departmentRequest.Name;
            ExistingDeprt.Description = departmentRequest.Description;
            ExistingDeprt.Type = departmentRequest.Type;
            ExistingDeprt.Email = departmentRequest.Email;
            ExistingDeprt.PhoneNumber = departmentRequest.PhoneNumber;

            _dbContext.Update(ExistingDeprt);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid ID)
        {
            var deprt = await _departmentRepo.GetByIDAsync(ID);

            if (deprt == null)
            {
                throw new ArgumentNullException(nameof(deprt));
            }

            _dbContext.Remove(deprt);
            await _dbContext.SaveChangesAsync();
        }
    }
}
