namespace HMC_Project.Models
{
    public class DepartmentAddress
    {
        public Guid DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public Guid AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}
