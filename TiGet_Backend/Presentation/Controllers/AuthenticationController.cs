using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.AspNetCore.Authentication;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public AuthenticationController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest registerDTO)
    {
        try
        {
            var token = await _customerService.Register(registerDTO);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CustomerLoginRequest loginDTO)
    {
        try
        {
            var token = await _customerService.Login(loginDTO);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
