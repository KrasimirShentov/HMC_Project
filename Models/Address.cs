namespace HMC_Project.Models
{
    public class Address
    {
        private string _address;
        public Address(string addressName)
        {
            ID = Guid.NewGuid();
            AddressName = addressName;
        }

        public Guid ID { get; private set; }
        public string AddressName {
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
    }
}
