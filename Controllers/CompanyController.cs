using HMC_Project.Interfaces.Services;
using HMC_Project.Models;
using HMC_Project.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HMC_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyInterface _companyService;

        public CompanyController(ICompanyInterface companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var company = await _companyService.GetByIDAsync(id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyRequest companyRequest)
        {
            var company = await _companyService.CreateAsync(companyRequest);
            return CreatedAtAction(nameof(GetByID), new { id = company.ID }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyRequest companyRequest)
        {
            await _companyService.UpdateAsync(id, companyRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
