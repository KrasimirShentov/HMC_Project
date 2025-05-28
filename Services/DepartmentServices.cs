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
            // The controller's ModelState.IsValid check should handle null departmentRequest,
            // but a defensive check here doesn't hurt.
            if (departmentRequest == null)
            {
                throw new ArgumentNullException(nameof(departmentRequest), "Department request data is missing.");
            }

            // 1. Check for existing department name within the specified company
            // Ensure you're checking uniqueness within the company context
            var existingDepartment = await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.Name == departmentRequest.Name && d.Company.ID == departmentRequest.CompanyID);

            if (existingDepartment != null)
            {
                throw new ArgumentException($"Department with the name '{departmentRequest.Name}' already exists for this company.");
            }

            // 2. Validate CompanyID and retrieve Company entity
            // No need for Guid.TryParse here, as model binding should ensure CompanyID is a valid Guid if [Required]
            var company = await _dbContext.Companies.FindAsync(departmentRequest.CompanyID);
            if (company == null)
            {
                throw new ArgumentException($"Company with ID '{departmentRequest.CompanyID}' not found.");
            }

            var newDepartment = new Department
            {
                Id = Guid.NewGuid(), // Generate new GUID for the entity
                Name = departmentRequest.Name,
                Description = departmentRequest.Description,
                Type = departmentRequest.Type,
                Email = departmentRequest.Email,
                PhoneNumber = departmentRequest.PhoneNumber,
                Company = company // Assign the found company entity
            };

            // 3. Handle Department Addresses
            // If DepartmentAddresses is [Required] in DTO, it won't be null here.
            // If it can be empty, remove [Required] from DTO and keep this check.
            if (departmentRequest.DepartmentAddresses != null && departmentRequest.DepartmentAddresses.Any())
            {
                foreach (var addressDto in departmentRequest.DepartmentAddresses)
                {
                    // Defensive check for null AddressDTO or empty AddressName
                    if (addressDto == null || string.IsNullOrWhiteSpace(addressDto.AddressName))
                    {
                        // Log a warning or throw a specific exception if malformed addresses are not allowed
                        Console.WriteLine($"Warning: Skipping invalid address entry (null DTO or empty AddressName).");
                        continue; // Skip this invalid entry
                    }

                    var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressName == addressDto.AddressName);
                    if (address == null)
                    {
                        address = new Address
                        {
                            AddressName = addressDto.AddressName
                        };
                        _dbContext.Addresses.Add(address); // Add new Address to DB if it doesn't exist
                    }

                    var departmentAddress = new DepartmentAddress
                    {
                        Department = newDepartment,
                        Address = address
                    };
                    newDepartment.DepartmentAddresses.Add(departmentAddress); // Add to the new Department's collection
                }
            }
            // else if (departmentRequest.DepartmentAddresses == null || !departmentRequest.DepartmentAddresses.Any())
            // {
            //     // If DepartmentAddresses is [Required] and empty/null is not allowed
            //     throw new ArgumentException("At least one department address is required.");
            // }


            _dbContext.Departments.Add(newDepartment); // Add the new department entity
            await _dbContext.SaveChangesAsync();

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

            // Clear existing department addresses before re-adding, for robust updates
            _dbContext.DepartmentAddresses.RemoveRange(ExistingDeprt.DepartmentAddresses);
            ExistingDeprt.DepartmentAddresses.Clear();

            if (departmentRequest.DepartmentAddresses != null && departmentRequest.DepartmentAddresses.Any())
            {
                foreach (var addressDto in departmentRequest.DepartmentAddresses) // Iterate over AddressDTO
                {
                    if (addressDto == null || string.IsNullOrWhiteSpace(addressDto.AddressName))
                    {
                        continue; // Skip invalid entries
                    }

                    var address = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressName == addressDto.AddressName);
                    if (address == null)
                    {
                        address = new Address { AddressName = addressDto.AddressName }; // Use AddressName from DTO
                        _dbContext.Addresses.Add(address);
                    }
                    ExistingDeprt.DepartmentAddresses.Add(new DepartmentAddress
                    {
                        Department = ExistingDeprt,
                        Address = address
                    });
                }
            }

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
