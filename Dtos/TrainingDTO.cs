using HMC_Project.Dtos;

public class TrainingDTO
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string PositionName { get; set; }
    public string Description { get; set; }
    public int TrainingHours { get; set; }
    public List<EmployeeDTO> Employees { get; set; }
}
