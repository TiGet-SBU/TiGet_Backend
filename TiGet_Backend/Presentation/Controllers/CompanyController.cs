using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{


    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {


        private readonly ICompanyService _companyService;
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(ICompanyService companyService, IUnitOfWork unitOfWork)
        {
            _companyService = companyService;
            _unitOfWork = unitOfWork;
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

        [HttpGet]
        [Route("stations")]
        public async Task<IEnumerable<Station>> GetAllStations()
        {
            return await _unitOfWork.StationRepository.GetAllAsync();
        }


    }
}
