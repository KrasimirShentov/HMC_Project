using HMC_Project.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Employee
    {
        public Guid ID { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]
        public string Email { get; set; }
        public string Position { get; set; }
        public GenderType Gender { get; set; }
        public virtual Training Training { get; set; }
        public virtual Department Department { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime HireDate { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }

        // Parameterless constructor required by EF Core
        public Employee()
        {
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }

        // Parameterized constructor for easier instantiation in code
        public Employee(Guid id, string name, string surname, int age, string email, string position)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Age = age;
            Email = email;
            Position = position;
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
    }
}
