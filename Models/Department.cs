using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMC_Project.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [Required, MaxLength(32)]
        public string Type { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(32)]
        public string PhoneNumber { get; set; }
        public List<Employee> employees { get; set; }
        public virtual ICollection<DepartmentAddress> DepartmentAddresses { get; private set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }

        public Department()
        {
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            employees = new List<Employee>();
        }

        public Department(string name, string description, string type, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
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
