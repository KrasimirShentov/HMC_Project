﻿using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class TrainingController : Controller
    {
        private readonly ITrainingInterface _trainingInterface;
        public TrainingController(ITrainingInterface trainingInterface)
        {
            _trainingInterface = trainingInterface;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByIDAsync(Guid ID)
        {
            try
            {
                var training = await _trainingInterface.GetByIDAsync(ID);
                return Ok(training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var training = await _trainingInterface.GetAllAsync();
                return Ok(training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TrainingRequest _trainingRequest)
        {
            try
            {
                var newTraining = await _trainingInterface.CreateAsync(_trainingRequest);
                return Ok(newTraining);
            }
            catch (ArgumentException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateAsync(Guid ID, [FromBody] TrainingRequest trainingRequest)
        {
            try
            {
                await _trainingInterface.UpdateAsync(ID, trainingRequest);
                return Ok();

            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
            }

        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteAsync(Guid ID) 
        {
            try
            {
                await _trainingInterface.DeleteAsync(ID);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}   
