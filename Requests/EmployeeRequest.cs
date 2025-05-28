using HMC_Project.Models;
using HMC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMC_Project.Requests
{
    public class EmployeeRequest
    {
        // public Guid ID = Guid.NewGuid();

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string Surname { get; set; }

        [Required] 
        [Range(18, 100)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Position { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GenderType Gender { get; set; }

        [Required] 
        public Guid DepartmentId { get; set; }

        [Required] 
        public Guid? TrainingId { get; set; }

        // If Birthday and HireDate are part of the request, uncomment and add [Required] if necessary
        // public DateTime Birthday { get; set; }
        // public DateTime HireDate { get; set; }
    }
}