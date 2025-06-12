using HMC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMC_Project.Models
{
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Surname { get; set; }
        [Required, MaxLength(32)]
        public int Age { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(32)]
        public string Position { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GenderType Gender { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid? TrainingID { get; set; }
        public Training Training { get; set; }
        public DateTime Birthday { get; set; }

        public DateTime HireDate { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public Employee()
        {
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
        public Employee(string name, string surname, int age, string email, string position, Training training, Department department)
        {
            ID = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Age = age;
            Email = email;
            Position = position;
            Department = department;
            Training = training;
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
    }
}
