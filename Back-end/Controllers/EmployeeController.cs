using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeInterface _employeeService;

        public EmployeeController(IEmployeeInterface employee)
        {
            _employeeService = employee;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(Guid ID)
        {
            try
            {
                var department = await _employeeService.GetByIDAsync(ID);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var deps = await _employeeService.GetAllAsync();
                return Ok(deps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newEmployee = await _employeeService.CreateAsync(employeeRequest);
                return CreatedAtAction(nameof(GetByID), new { ID = newEmployee.ID }, newEmployee);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    return Conflict(ex.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"An internal server error occurred: {ex.Message}");
            //}
            catch (Exception ex)
            {
                throw new Exception("Failed to create employee: " + ex.Message + " | Inner: " + ex.InnerException?.Message, ex);
            }
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> Update(Guid ID, EmployeeRequest employeeRequest)
        {
            try
            {
                await _employeeService.UpdateAsync(ID, employeeRequest);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(Guid ID)
        {
            try
            {
                await _employeeService.DeleteAsync(ID);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}
