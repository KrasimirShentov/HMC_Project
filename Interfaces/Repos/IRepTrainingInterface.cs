﻿using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepTrainingInterface
    {
        Task<TrainingDTO> GetByIDAsync(Guid TrainingID);
        Task<IEnumerable<TrainingDTO>> GetAllAsync();

        Task<Training> CreateAsync(Training training);
        Task UpdateAsync(Training training);
        Task DeleteAsync(Training training);
    }
}
