using System;
using System.Collections.Generic;

namespace HMC_Project.Models
{
    public class Company
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
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
