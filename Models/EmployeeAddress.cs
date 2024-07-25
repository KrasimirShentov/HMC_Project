using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class EmployeeAddress
    {
        [ForeignKey(nameof(Employee))]
        public string EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(Address))]
        public string AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
