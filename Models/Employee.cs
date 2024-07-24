using HMC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Employee
    {
        private string _name;
        private string _surname;
        private string _email;
        public Guid ID { get; private set; }
        public string Name {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                throw new ArgumentNullException("Name can't be null or empty");
            }
        }
        public string Surname {
            get { return _surname; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _surname = value;
                }
                throw new ArgumentNullException("Surname can't be null or empty");
            }
        }
        public int Age { get; set; }

        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]
        public string Email {
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
        public string Position { get; set; }
        public Training Training { get; set; }
        public GenderType Gender {  get; set; }
        public DateTime Birthday { get; set; }
        public DateTime HireDate { get; set; }

        public Employee(string name, string surname, int age, string email, string position, Training training)
        {
            ID = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Age = age;
            Email = email;
            Position = position;
            Training = training;
        }
    }
}
