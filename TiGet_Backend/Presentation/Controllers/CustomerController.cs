﻿using Microsoft.AspNetCore.Mvc;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Services;
using Application.DTOs.TicketDTO;

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
    public async Task<IActionResult> Register([FromForm] CustomerRegisterRequest req)
    {
        try
        {
            var response = await _customerService.Register(req);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] CustomerLoginRequest req)
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
