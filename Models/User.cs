using HMC_Project.Models.Enums;
using System.Diagnostics.CodeAnalysis;

namespace HMC_Project.Models
{
    public class User
    {
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private string _email;

        public Guid ID { get; private set; }

        public string Name { get { return _name; } set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                throw new ArgumentNullException("Name can't be null or empty");
            }
        }
        public string Surname { get { return _surname; } set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _surname = value;
                }
                throw new ArgumentNullException("Surname can't be null or empty");
            }
        }
        public string UserName { get { return _username; } set {
                if (!string.IsNullOrEmpty(value))
                {
                    _username = value;
                }
                throw new ArgumentNullException("Username can't be null or empty");
            } }
        public string Password
        {
            get { return _password; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _password = value;
                }
                throw new ArgumentNullException("Password can't be null or empty");
            } }
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
            } }
        public GenderType Gender { get; set; }

        public DateTime CreatedOn = DateTime.UtcNow;
        public DateTime DateOfBirth { get { return DateOfBirth.Date; } set { DateOfBirth = value.Date; } }
    }
}
