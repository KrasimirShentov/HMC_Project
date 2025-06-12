namespace HMC_Project.Dtos
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DepartmentDTO> Departments { get; set; }
        public List<AddressDTO> Addresses { get; set; }
    }
}
