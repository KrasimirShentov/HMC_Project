using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Company
    {
        [Key]
        public Guid ID { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        public Company()
        {
            Departments = new HashSet<Department>();
            Addresses = new HashSet<Address>();
        }
        public Company(string name, string description)
        {
            ID = Guid.NewGuid();
            Name = name;
            Description = description;
        }
    }
}
