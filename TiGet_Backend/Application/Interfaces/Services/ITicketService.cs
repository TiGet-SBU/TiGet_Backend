using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.TicketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITicketService
    {
        public Task<TicketAddResponse> AddTicket(TicketAddRequest request);
        public Task<TicketGetByIdResponse> GetTicketById(TicketGetByIdRequest request);
        public Task<TicketGetAllResponse> GetAllTickets(TicketGetAllRequest request);
        public Task<TicketUpdateResponse> UpdateTicket(TicketUpdateRequest request);
        public Task<TicketDeleteResponse> DeleteTicket(TicketDeleteRequest request);

    }
}
