﻿using HMC_Project.Interfaces.Repos;
using HMC_Project.Models;
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
        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _dbContext.Training.ToListAsync();
        }

        public async Task<Training> CreateAsync(Training training)
        {
            _dbContext.Training.Add(training);
            _dbContext.SaveChangesAsync();
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
