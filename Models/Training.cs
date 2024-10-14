using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMC_Project.Models
{
    public class Training
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int TrainingHours { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Training()
        {
            
        }
        public Training(Guid Id, string type, string positionName, string description, int trainingHours)
        {
            ID = Id;
            Type = type;
            PositionName = positionName;
            Description = description;
            TrainingHours = trainingHours;
        }
    }
}
