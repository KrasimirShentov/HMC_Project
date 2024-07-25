namespace HMC_Project.Models
{
    public class Department
    {
        private string _name;
        private string _description;
        private string _Type;
        private string _email;
        public Guid Id { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                throw new ArgumentNullException("Name can't be null of empty");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _description = value;
                }
                throw new ArgumentNullException("Description can't be null or empty");
            }
        }
        public string Type
        {
            get { return _Type; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Type = value;
                }
                throw new ArgumentNullException("Type can't be null or empty");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _email = value;
                }
                throw new ArgumentNullException("Email can't be null or empty");
            }
        }

        public string PhoneNumber { get; set; }
        public List<Employee> employees { get; set; }
        public virtual ICollection<DepartmentAddress> DepartmentAddresses { get; private set; }
        public Department(string name, string description, string type, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Type = type;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentAddresses = new HashSet<DepartmentAddress>();
            employees = new List<Employee>();
        }
    }
}
