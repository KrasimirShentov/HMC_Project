using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;

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
        public async Task<DepartmentDTO> GetByIDAsync(Guid DepartmentID)
        {
            var result = await _departmentRepo.GetByIDAsync(DepartmentID);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            return await _departmentRepo.GetAllAsync();
        }
        public async Task<Department> CreateAsync(DepartmentRequest departmentRequest)
        {
            var existingDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Name == departmentRequest.Name);
            if (existingDepartment != null)
            {
                throw new ArgumentException("Department with the same name already exists in this company.");
            }

            var company = await _dbContext.Companies.FindAsync(departmentRequest.CompanyID);
            if (company == null)
            {
                throw new ArgumentException("Invalid Company ID");
            }

            var newDepartment = new Department
            {
                Id = Guid.NewGuid(),
                Name = departmentRequest.Name,
                Description = departmentRequest.Description,
                Type = departmentRequest.Type,
                Email = departmentRequest.Email,
                PhoneNumber = departmentRequest.PhoneNumber,
                Company = company
            };

            //if (departmentRequest.DepartmentAddresses != null && departmentRequest.DepartmentAddresses.Count > 0)
            //{
                foreach (var addressName in departmentRequest.DepartmentAddresses)
                {
                    var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressName == addressName);
                    if (address == null)
                    {
                        address = new Address
                        {
                            AddressName = addressName
                        };
                        _dbContext.Addresses.Add(address);
                    }

                    var departmentAddress = new DepartmentAddress
                    {
                        Department = newDepartment,
                        Address = address
                    };
                    _dbContext.DepartmentAddresses.Add(departmentAddress);
                }
            //}

            //try
            //{
                _dbContext.Add(newDepartment);
                await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.InnerException?.Message);
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}

            return newDepartment;
        }
        public async Task UpdateAsync(Guid ID, DepartmentRequest departmentRequest)
        {
            var ExistingDeprt = await _dbContext.Departments
                .Include(d => d.DepartmentAddresses)
                .ThenInclude(da => da.Address)
                .FirstOrDefaultAsync(d => d.Id == ID);
            if (ExistingDeprt == null)
            {
                throw new ArgumentNullException("Department not found");
            }

            ExistingDeprt.Name = departmentRequest.Name;
            ExistingDeprt.Description = departmentRequest.Description;
            ExistingDeprt.Type = departmentRequest.Type;
            ExistingDeprt.Email = departmentRequest.Email;
            ExistingDeprt.PhoneNumber = departmentRequest.PhoneNumber;

            //?????????????????????????????????????????????????????????????????
            var existingDepartmentAddresses = ExistingDeprt.DepartmentAddresses.ToList();

            for (int i = 0; i < existingDepartmentAddresses.Count; i++)
            {
                var departmentAddress = existingDepartmentAddresses[i];

                string newAddressName = departmentRequest.DepartmentAddresses.ElementAtOrDefault(i);

                if (newAddressName != null)
                {
                    departmentAddress.Address.AddressName = newAddressName;
                }
            }
            //    else
            //    {

            //        var existingAddress = await _dbContext.Addresses
            //            .FirstOrDefaultAsync(a => a.AddressName == newAddressName);

            //        if (existingAddress != null)
            //        {
            //            departmentAddress = new DepartmentAddress
            //            {
            //                Department = ExistingDeprt,
            //                Address = existingAddress
            //            };
            //            ExistingDeprt.DepartmentAddresses.Add(departmentAddress);
            //        }
            //     }     
        

            _dbContext.Update(ExistingDeprt);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid ID)
        {
            var deprt = await _dbContext.Departments.FindAsync(ID);

            if (deprt == null)
            {
                throw new ArgumentNullException(nameof(deprt));
            }

            _dbContext.Remove(deprt);
            await _dbContext.SaveChangesAsync();
        }
    }
}
