using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public class TicketUpdateResponse
    {
        public Guid Id { get; set; }
        public DateTime TimeToGo { get; set; }
        public double Price { get; set; }
        // Add other relevant properties
    }
}
