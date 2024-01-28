using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO.Auth
{
    public record RegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordRepeat { get; set; }
        public required Role Role { get; set; }
    }
}
