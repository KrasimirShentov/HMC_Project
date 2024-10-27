using HMC_Project.Models.Enums;

namespace HMC_Project.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public GenderType Gender { get; set; }
        public virtual Training Training { get; set; }
        public virtual Department Department { get; set; }
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
