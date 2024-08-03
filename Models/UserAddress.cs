using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class UserAddress
    {
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public Guid UserAddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
