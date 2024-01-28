using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public class TicketUpdateRequest
    {
        public int Id { get; set; }
        public DateTime TimeToGo { get; set; }
        public double Price { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? SourceId { get; set; }
        public Guid? DestinationId { get; set; }
    }
}
