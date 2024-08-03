using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Address
    {
        private string _address;

        [Key]
        public Guid ID { get; private set; }
        public string AddressName
        {
            get
            {
                return _address;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address = value;
                }
                throw new ArgumentNullException("Address can't be null or empty");
            }
        }

        public virtual ICollection<Department> DepartmentAddresses { get; private set; }
        public virtual ICollection<Employee> EmployeeAddresses { get; private set; }
        public virtual ICollection<User> UserAddresses { get; private set; }
        public Address(string addressName)
        {
            ID = Guid.NewGuid();
            AddressName = addressName;
            DepartmentAddresses = new HashSet<Department>();
            EmployeeAddresses = new HashSet<Employee>();
            UserAddresses = new HashSet<User>();
        }
    }
}
