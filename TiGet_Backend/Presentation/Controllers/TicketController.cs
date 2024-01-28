using Microsoft.AspNetCore.Mvc;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;
using Application.DTOs.TicketDTO;
using Application.DTOs.CustomerDTO;

[Route("api/ticket")] // Adjusted route for clarity
[ApiController]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddTicket([FromBody] TicketAddRequest request)
    {
        try
        {
            var response = await _ticketService.AddTicket(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTicketById(Guid id)
    {
        try
        {
            var response = await _ticketService.GetTicketById(new TicketGetByIdRequest { Id = id });
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        try
        {
            var response = await _ticketService.GetAllTickets(new TicketGetAllRequest());
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketUpdateRequest request)
    {
        try
        {
            request.Id = id; // Ensure ID consistency
            var response = await _ticketService.UpdateTicket(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTicket(Guid id)
    {
        try
        {
            var response = await _ticketService.DeleteTicket(new TicketDeleteRequest { Id = id });
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}