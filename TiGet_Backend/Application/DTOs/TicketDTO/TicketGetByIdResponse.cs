using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public class TicketGetByIdResponse
    {
        public Ticket Ticket { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
