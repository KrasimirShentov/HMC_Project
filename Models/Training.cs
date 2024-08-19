using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMC_Project.Models
{
    public class Training
    {
        public Guid ID { get; private set; }
        public string Type { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int TrainingHours { get; set; }
        public Training()
        {
            
        }
        public Training(string type, string positionName, string description, int trainingHours)
        {
            ID = Guid.NewGuid();
            Type = type;
            PositionName = positionName;
            Description = description;
            TrainingHours = trainingHours;
        }
    }
}
