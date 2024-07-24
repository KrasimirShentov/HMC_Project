using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class DepartmentAddress
    {

        [ForeignKey(nameof(Department))]
        public string DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(Address))]
        public string AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
