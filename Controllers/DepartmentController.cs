using HMC_Project.Interfaces.Services;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentInterface _departmentService;

        public DepartmentController(IDepartmentInterface department)
        {
            _departmentService = department;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(Guid ID)
        {
            try
            {
                var department = await _departmentService.GetByIDAsync(ID);
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
                var deps = await _departmentService.GetAllAsync();
                return Ok(deps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentRequest _departmentRequest)
        {
            try
            {
                await _departmentService.CreateAsync(_departmentRequest);
                return Ok(_departmentRequest);
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
        public async Task<IActionResult> Update(Guid ID, DepartmentRequest departmentRequest)
        {
            try
            {
                await _departmentService.UpdateAsync(ID, departmentRequest);
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
                await _departmentService.DeleteAsync(ID);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}