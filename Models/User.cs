using HMC_Project.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentNullException("Name can't be null or empty");
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _surname = value;
                }
                else
                {
                    throw new ArgumentNullException("Surname can't be null or empty");
                }
            }
        }

        public string UserName
        {
            get { return _username; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _username = value;
                }
                else
                {
                    throw new ArgumentNullException("Username can't be null or empty");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentNullException("Password can't be null or empty");
                }
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
                else
                {
                    throw new ArgumentNullException("Email can't be null or empty");
                }
            }
        }

        public GenderType Gender { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
        public DateTime DateOfBirth{get; set;}

        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public User()
        {
            UserAddresses = new HashSet<UserAddress>();
        }

        public User(Guid id, string name, string surname, string username, string password, string email)
        {
            ID = id;
            Name = name;
            Surname = surname;
            UserName = username;
            Password = password;
            Email = email;
            UserAddresses = new HashSet<UserAddress>();
        }
    }
}
