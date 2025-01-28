using HMC_Project.Dtos;
using HMC_Project.Models;
using HMC_Project.Models.Enums;

public class EmployeeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age{ get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public GenderType Gender { get; set; }
    public DepartmentDTO DepartmentDTO { get; set; }
    public TrainingDTO TrainingDTO { get; set; }
}
