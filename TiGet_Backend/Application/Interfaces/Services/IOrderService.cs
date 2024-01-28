using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<AddOrderResponse> AddOrder(AddOrderRequest req);
    }
}
