namespace HMC_Project.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Employee> employees { get; set; }
        public virtual ICollection<DepartmentAddress> DepartmentAddresses { get; private set; }
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public Department()
        {
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            employees = new List<Employee>();
        }

        public Department(string name, string description, string type, string email, string phoneNumber, Guid companyID)
        {
            Id = Guid.NewGuid();
            CompanyID = companyID;
            Name = name;
            Description = description;
            Type = type;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            employees = new List<Employee>();
        }
    }
}
