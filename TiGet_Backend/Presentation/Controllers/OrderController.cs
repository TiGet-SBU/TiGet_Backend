using Application.DTOs.OrderDTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [Route("api/order")] // Adjusted route for clarity
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequest request)
        {
            try
            {
                var response = await _orderService.AddOrder(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
