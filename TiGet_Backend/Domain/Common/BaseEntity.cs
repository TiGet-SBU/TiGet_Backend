using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public record BaseEntity
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
