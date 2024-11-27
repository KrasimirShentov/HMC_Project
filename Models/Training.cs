using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace HMC_Project.Models
{
    public class Training
    {
        [Key]
        public Guid ID { get; set; }
        [Required, MaxLength(32)]
        public string Type { get; set; }
        [Required, MaxLength(32)]
        public string PositionName { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int TrainingHours { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Training()
        {
            Employees = new HashSet<Employee>();
        }
        public Training(string type, string positionName, string description, int trainingHours)
        {
            ID = Guid.NewGuid();
            Type = type;
            PositionName = positionName;
            Description = description;
            TrainingHours = trainingHours;
            Employees = new HashSet<Employee>();
        }
    }
}
