using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO;
using Application.DTOs.TicketDTO;
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
        private readonly ITicketService _ticketService;
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(ICompanyService companyService, IUnitOfWork unitOfWork, ITicketService ticketService)
        {
            _companyService = companyService;
            _unitOfWork = unitOfWork;
            _ticketService = ticketService;
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
        public async Task<IActionResult> GetAllStations()
        {
            return Ok(await _unitOfWork.StationRepository.GetAllAsync());
        }

        [HttpPost]
        [Route("addTicket")]
        public async Task<IActionResult> AddTicket(TicketAddRequest req)
        {
            try
            {
                var response = await _ticketService.AddTicket(req);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
