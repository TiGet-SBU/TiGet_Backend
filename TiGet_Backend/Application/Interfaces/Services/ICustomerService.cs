using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.CustomerDTO;
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
        public Task<RegisterResponse> Register(RegisterRequest req);
        public Task<CustomerLoginResponse> Login(CustomerLoginRequest req);
        public Task<IEnumerable<TicketGetResponse>> GetAllTickets(TicketGetAllRequest req);
        public Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest req); 
    }
}
