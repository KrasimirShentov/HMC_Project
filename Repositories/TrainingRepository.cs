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
        public async Task<Training> GetByIDAsync(Guid TrainingID)
        {
            return await _dbContext.Training.FindAsync(TrainingID);
        }
        public async Task<IEnumerable<TrainingRequest>> GetAllAsync()
        {
            var trainings = _dbContext.Training
        .Select(t => new TrainingRequest
        {
            ID = t.ID,
            Type = t.Type,
            PositionName = t.PositionName,
            Description = t.Description,
            TrainingHours = t.TrainingHours,
            Employees = t.Employees
                .Select(e => new EmployeeRequest
                {
                    ID = e.ID,
                    Name = e.Name,
                    Surname = e.Surname,
                    Email = e.Email,
                    Position = e.Position,
                })
                .ToList()
        })
        .ToList();

            return trainings;
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
