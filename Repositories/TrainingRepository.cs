using HMC_Project.Dtos;
using HMC_Project.Interfaces.Repos;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HMC_Project.Repositories
{
    public class TrainingRepository : IRepTrainingInterface
    {
        private readonly HMCDbContext _dbContext;
        public TrainingRepository(HMCDbContext hMCDbContext)
        {
            _dbContext = hMCDbContext;
        }
        public async Task<TrainingDTO> GetByIDAsync(Guid TrainingID)
        {
            return await _dbContext.Training
        .Include(t => t.Employees)
        .ThenInclude(e => e.Department)
        .Where(t => t.ID == TrainingID)
        .Select(t => new TrainingDTO
        {
            Id = t.ID,
            Type = t.Type,
            PositionName = t.PositionName,
            Description = t.Description,
            TrainingHours = t.TrainingHours,
            Employees = t.Employees.Select(e => new EmployeeDTO
            {
                Id = e.ID,
                Name = e.Name,
                Surname = e.Surname,
                Age = e.Age,
                Email = e.Email,
                Position = e.Position,
                Gender = e.Gender,
                DepartmentDTO = new DepartmentDTO
                {
                    ID = e.Department.Id,
                    Name = e.Department.Name,
                    Email = e.Department.Email,
                    Type = e.Department.Type,
                    PhoneNumber = e.Department.PhoneNumber,
                    Description = e.Department.Description
                }
            }).ToList()
        })
        .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TrainingDTO>> GetAllAsync()
        {
            return await _dbContext.Training
                .Include(t => t.Employees)
                .Select(t => new TrainingDTO
                {
                    Id = t.ID,
                    Type = t.Type,
                    PositionName = t.PositionName,
                    Description = t.Description,
                    TrainingHours = t.TrainingHours,
                    Employees = t.Employees
                    .Select(e => new EmployeeDTO
                    {
                        Id = e.ID,
                        Name = e.Name,
                        Surname = e.Surname,
                        Email = e.Email,
                        Position = e.Position
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Training> CreateAsync(Training training)
        {
            _dbContext.Training.Add(training);
            await _dbContext.SaveChangesAsync();
            return training;
        }
        public async Task UpdateAsync(Training training)
        {
            _dbContext.Entry(training).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Training training)
        {
            _dbContext.Training.Remove(training);
            await _dbContext.SaveChangesAsync();
        }
    }
}
