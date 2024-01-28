using Microsoft.AspNetCore.Mvc;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;
using Application.DTOs.TicketDTO;
using Application.DTOs.CustomerDTO;

[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest req)
    {
        try
        {
            var response = await _customerService.UpdateCustomer(req);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("tickets")]
    public async Task<IActionResult> GetTickets(TicketGetAllRequest req)
    {
        try
        {
            var resposne = await _customerService.GetAllTickets(req);
            return Ok(resposne);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
