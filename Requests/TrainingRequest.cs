namespace HMC_Project.Requests
{
    public class TrainingRequest
    {
        public Guid ID { get; private set; }
        public string Type { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int TrainingHours { get; set; }
    }
}
