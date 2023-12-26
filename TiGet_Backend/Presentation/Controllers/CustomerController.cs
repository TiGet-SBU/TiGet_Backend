using Microsoft.AspNetCore.Mvc;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;

[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    
    [HttpPost]
    [Route("singup")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest registerDTO)
    {
        try
        {
            var response = await _customerService.Register(registerDTO);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] CustomerLoginRequest loginDTO)
    {
        try
        {
            var response = await _customerService.Login(loginDTO);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
