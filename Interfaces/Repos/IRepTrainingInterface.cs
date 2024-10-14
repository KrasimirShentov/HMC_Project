using HMC_Project.Models;

namespace HMC_Project.Interfaces.Repos
{
    public interface IRepTrainingInterface
    {
        Task<Training> GetByIDAsync(Guid TrainingID);
        Task<IEnumerable<Training>> GetAllAsync();
        Task<Training> CreateAsync(Training training);
        Task UpdateAsync(Training training);
        Task DeleteAsync(Training training);
    }
}
