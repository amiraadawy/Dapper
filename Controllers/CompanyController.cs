using DapperAsp.Net.Contract;
using DapperAsp.Net.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperAsp.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();
            return Ok(companies);
        }
        [HttpGet("{id}", Name =" Get Company BY ID")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompany(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyForcreationDTO company)
        {
            var createdCompany = await _companyRepository.CreateCompany(company);
            return CreatedAtRoute(" Get Company BY ID", new { id = createdCompany.Id }, createdCompany);
            //if (createdCompany == null)
            //    return BadRequest();
            //return Ok(createdCompany);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdate company)
        {
            var updatedCompany = await _companyRepository.UpdateCompany(id, company);
            if (updatedCompany == null)
                return NotFound();
            return Ok(updatedCompany);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _companyRepository.DeleteCompany(id);
            if (!result)
                return NotFound();
            return Ok(" Deleted");
        }
        [HttpGet("GetCompanyByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompanyByEmployeeId(int id)
        {
            var company = await _companyRepository.GetCompanyByEmployeeId(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        [HttpGet("GetMulipleResult/{id}")]
        public async Task<IActionResult> GetMulipleResult(int id)
        {
            var company = await _companyRepository.GetMulipleResult(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        [HttpGet("Get All Companies Has Employess")]
        public async Task<IActionResult> MultipleMapping()
        {
            var company = await _companyRepository.GetMulipleMapping();
            if (company == null)
                return NotFound();
            return Ok(company);

        }
        [HttpPost("CreateMultipleCompanies")]
        public async Task<IActionResult> CreateMultipleCompanies([FromBody] List<CompanyForcreationDTO> companies)
        {
            if (companies == null || !companies.Any())
                return BadRequest("Company list cannot be empty.");

            try
            {
                var createdCompanies = await _companyRepository.CreateMultipleCompany(companies);
                return Ok(createdCompanies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
