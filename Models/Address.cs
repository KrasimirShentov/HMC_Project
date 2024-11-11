using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Address
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string AddressName { get; set; }
        public virtual ICollection<DepartmentAddress> DepartmentAddresses { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public Address(string addressName, Guid userID)
        {
            ID = Guid.NewGuid();
            UserID = userID;
            AddressName = addressName;
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            EmployeeAddresses = new HashSet<EmployeeAddress>();

        }
        public Address()
        {
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            EmployeeAddresses = new HashSet<EmployeeAddress>();
        }
    }
}
