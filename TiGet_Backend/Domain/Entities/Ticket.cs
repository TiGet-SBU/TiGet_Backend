using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        public required DateTime TimeToGo { get; set; }
        public required double Price { get; set; }

        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public Guid CompanyId { get; set; }
        public Company? Company{ get; set;}

        public required Guid SourceId { get; set; }
        public Station Source { get; set; }

        public required Guid DestinationId { get; set; }
        public Station Destination { get; set; }

    }
}
