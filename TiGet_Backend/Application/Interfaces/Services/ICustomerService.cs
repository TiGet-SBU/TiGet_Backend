using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.TicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICustomerService
    {
        public Task<CustomerRegisterResponse> Register(CustomerRegisterRequest req);
        public Task<CustomerLoginResponse> Login(CustomerLoginRequest req);
        public Task<IEnumerable<TicketGetResponse>> GetAllTickets();

    }
}
