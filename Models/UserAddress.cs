using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class UserAddress
    {
        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Address))]
        public string UserAddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
