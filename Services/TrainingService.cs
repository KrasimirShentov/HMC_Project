﻿using HMC_Project.Interfaces.Repos;
using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;

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
        public async Task<Training> GetByIDAsync(Guid TrainingID)
        {
            var result = await _repTrainingInterface.GetByIDAsync(TrainingID);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
        public async Task<IEnumerable<Training>> GetAllAsync()
        {
             return await _repTrainingInterface.GetAllAsync();
        }
        public async Task<Training> CreateAsync(TrainingRequest trainingRequest)
        {
            var newTraining = new Training
            {
                ID = trainingRequest.ID,
                Type = trainingRequest.Type,
                PositionName = trainingRequest.PositionName,
                Description = trainingRequest.Description,
                TrainingHours = trainingRequest.TrainingHours
            };

            _dbContext.Add(newTraining);
            await _dbContext.SaveChangesAsync();

            return newTraining;
        }
        public async Task UpdateAsync(Training training)
        {
            var existingTraining = await _repTrainingInterface.GetByIDAsync(training.ID);
            if (existingTraining == null)
            {
                throw new ArgumentNullException(nameof(training));
            }

            existingTraining.Type = training.Type;
            existingTraining.PositionName = training.PositionName;
            existingTraining.Description = training.Description;
            existingTraining.TrainingHours = training.TrainingHours;

            _dbContext.Update(existingTraining);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Training training)
        {
            var _training = await _repTrainingInterface.GetByIDAsync(training.ID);

            if (_training == null)
            {
                throw new ArgumentNullException(nameof(_training));
            }

            _dbContext.Remove(_training);
            await _dbContext.SaveChangesAsync();
        }
    }
}
