using HMC_Project.Models;
using HMC_Project.Requests;

namespace HMC_Project.Interfaces.Services
{
    public interface ITrainingInterface
    {
        Task<TrainingDTO> GetByIDAsync(Guid TrainingID);
        Task<IEnumerable<TrainingDTO>> GetAllAsync();
        Task<Training> CreateAsync(TrainingRequest trainingRequest);
        Task UpdateAsync(Guid ID, TrainingRequest trainingRequest);
        Task DeleteAsync(Guid ID);
    }
}
