using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class EmployeeAddress
    {
        public Guid EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public Guid AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
