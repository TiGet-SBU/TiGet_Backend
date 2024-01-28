using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CityDTO
{
    public class CityAddResponse
    {
        public required string CityName { get; set; }
        public required string Province { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
