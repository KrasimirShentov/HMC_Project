using HMC_Project.Models;

namespace HMC_Project.Dtos
{
    public class DepartmentDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public List<AddressDTO> Addresses { get; set; }
        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public List<EmployeeDTO>? Employees { get; set; }
    }
}
