using HMC_Project.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
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
        public Employee()
        {
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
        public Employee(Guid id, string name, string surname, int age, string email, string position, /*Training training, */Department department)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Age = age;
            Email = email;
            Position = position;
            Training = Training;
            Department = department;
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
    }
}
