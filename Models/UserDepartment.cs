using System.ComponentModel.DataAnnotations.Schema;

namespace HMC_Project.Models
{
    public class UserDepartment
    {
        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Department))]
        public string DepartmentID { get; set; }
        public string Department { get; set; }
    }
}
