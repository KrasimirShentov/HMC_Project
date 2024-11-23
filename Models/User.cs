using HMC_Project.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [Required, MaxLength(32)]
        public string Name { get; set; }

        [Required, MaxLength(32)]
        public string Surname { get; set; }

        [Required, MaxLength(32)]
        public string UserName { get; set; }

        [Required, MaxLength(24)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public User()
        {
            Addresses = new HashSet<Address>();
        }
        public User(Guid id, string name, string surname, string username, string password, string email)
        {
            ID = id;
            Name = name;
            Surname = surname;
            UserName = username;
            Password = password;
            Email = email;
            Addresses = new HashSet<Address>();
        }
    }
}
