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
        public async Task<IActionResult> GetByID(Guid employeeID)
        {
            try
            {
                var department = await _employeeService.GetByIDAsync(employeeID);
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
        public async Task<IActionResult> Create([FromBody] EmployeeRequest _employeeRequest)
        {
            try
            {
                await _employeeService.CreateAsync(_employeeRequest);
                return Ok(_employeeRequest);
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
