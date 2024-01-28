using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        public AuthController(ICustomerService customerService, ICompanyService companyService)
        {
            _customerService = customerService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("singup")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            try
            {
                if (req.Role == Domain.Enums.Role.Customer)
                {
                    var response1 = await _customerService.Register(req);
                    if (response1 == null) return BadRequest("Register falied!");

                    var response2 = await _customerService.Login(new CustomerLoginRequest { Email = req.Email, Password = req.Password });
                    return Ok(response2);
                } else if (req.Role == Domain.Enums.Role.Compony)
                {
                    var response1 = await _companyService.Register(req);
                    if (response1 == null) return BadRequest("Register falied!");

                    var response2 = await _companyService.Login(new CompanyLoginRequest { Email = req.Email, Password = req.Password });
                    return Ok(response2);
                }
                else
                {
                    return BadRequest("role not valid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(CustomerLoginRequest req)
        {
            try
            {
                var response = await _customerService.Login(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}
