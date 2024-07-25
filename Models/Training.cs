using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMC_Project.Models
{
    public class Training
    {
        public string Type { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int TrainingHours { get; set; }

        public Training(string type, string positionName, string description, int trainingHours)
        {
            Type = type;
            PositionName = positionName;
            Description = description;
            TrainingHours = trainingHours;
        }
    }
}
