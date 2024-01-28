using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CityDTO
{
    public class CityAddRequest
    {
        public required string CityName { get; set; }
        public required string Province { get; set; }

    }
}
