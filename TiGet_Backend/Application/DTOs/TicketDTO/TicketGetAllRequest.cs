using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public record TicketGetAllRequest
    {
        public required int first = 0;
        public required int last = int.MaxValue;
        public required Expression<Func<Ticket, bool>>? condition = null;
    }
}
