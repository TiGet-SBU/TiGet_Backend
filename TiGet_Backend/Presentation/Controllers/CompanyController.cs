using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{


    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {


        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(UpdateCompanyRequest req)
        {
            try
            {
                var response = await _companyService.UpdateCompany(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
