using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public record TicketGetResponse
    {
        public required DateTime TimeToGo { get; set; }
        public required double Price { get; set; }
        public required Vehicle Vehicle { get; set; }
        public required Company Company { get; set; }
        public required Station Source { get; set; }
        public required Station Destination { get; set; }
    }
}
