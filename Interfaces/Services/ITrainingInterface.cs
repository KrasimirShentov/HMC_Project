using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface ITrainingInterface
    {
        Task<Training> GetByIDAsync(Guid TrainingID);
        Task<IEnumerable<Training>> GetAllAsync();
        Task<Training> CreateAsync(TrainingRequest trainingRequest);
        Task UpdateAsync(Training training);
        Task DeleteAsync(Training training);
    }
}
