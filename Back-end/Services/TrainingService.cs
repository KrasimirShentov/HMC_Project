using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.EntityFrameworkCore;

namespace HMC_Project.Services
{
    public class TrainingService : ITrainingInterface
    {
        private readonly IRepTrainingInterface _repTrainingInterface;
        private readonly HMCDbContext _dbContext;

        public TrainingService(IRepTrainingInterface repTrainingInterface, HMCDbContext hMCDbContext)
        {
            _repTrainingInterface = repTrainingInterface;
            _dbContext = hMCDbContext;
        }
        public async Task<TrainingDTO> GetByIDAsync(Guid TrainingID)
        {
            var result = await _repTrainingInterface.GetByIDAsync(TrainingID);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
        public async Task<IEnumerable<TrainingDTO>> GetAllAsync()
        {
            return await _repTrainingInterface.GetAllAsync();
        }
        public async Task<Training> CreateAsync(TrainingRequest trainingRequest)
        {
            var existingTraining = await _dbContext.Training
        .FirstOrDefaultAsync(t => t.PositionName == trainingRequest.PositionName);
            if (existingTraining != null)
            {
                throw new ArgumentException("Training with the same type and position name already exists.");
            }

            var newTraining = new Training
            {
                //ID = trainingRequest.ID,
                Type = trainingRequest.Type,
                PositionName = trainingRequest.PositionName,
                Description = trainingRequest.Description,
                TrainingHours = trainingRequest.TrainingHours
            };

            _dbContext.Add(newTraining);
            await _dbContext.SaveChangesAsync();

            return newTraining;
        }
        public async Task UpdateAsync(Guid ID, TrainingRequest trainingRequest)
        {
            var existingTraining = await _dbContext.Training.FindAsync(ID);
            if (existingTraining == null)
            {
                throw new ArgumentNullException(nameof(trainingRequest));
            }

            existingTraining.Type = trainingRequest.Type;
            existingTraining.PositionName = trainingRequest.PositionName;
            existingTraining.Description = trainingRequest.Description;
            existingTraining.TrainingHours = trainingRequest.TrainingHours;

            _dbContext.Update(existingTraining);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid TrainingID)
        {
            var _training = await _dbContext.Training.FindAsync(TrainingID);

            if (_training == null)
            {
                throw new ArgumentNullException(nameof(_training));
            }

            _dbContext.Remove(_training);
            await _dbContext.SaveChangesAsync();
        }
    }
}
