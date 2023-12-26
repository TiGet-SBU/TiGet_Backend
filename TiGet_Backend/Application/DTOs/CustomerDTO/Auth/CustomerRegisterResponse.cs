using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO.Auth
{
    public record  CustomerRegisterResponse
    {
        public required string Email { get; set; }
        public required Role Role { get; set; }

    }
}
