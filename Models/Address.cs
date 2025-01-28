using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Address
    {
        [Key]
        [Required, MaxLength(255)]
        public string AddressName { get; set; }

        public virtual ICollection<DepartmentAddress> DepartmentAddresses { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public Guid CompanyID { get; set; }
        public Company Company { get; set; }
        public Address()
        {
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
        public Address(string addressName, Guid userID)
        { 
            UserID = userID;
            AddressName = addressName;
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            EmployeeAddresses = new HashSet<EmployeeAddress>();

        }
    }
}
