using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingInterface _trainingInterface;
        public TrainingController(ITrainingInterface trainingInterface)
        {
            _trainingInterface = trainingInterface;
        }

        //[HttpGet("{ID}")]
        //public async Task<IActionResult> GetByIDAsync(Guid TrainingID) { }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var training = _trainingInterface.GetAllAsync();
                return Ok(training);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateAsync([FromBody] TrainingRequest _trainingRequest){}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(Training training) { }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAsync(Training training) { }
    }
}
