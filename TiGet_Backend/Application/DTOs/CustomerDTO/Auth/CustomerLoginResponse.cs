using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO.Auth
{
    public record CustomerLoginResponse
    {
        public required string Email { get; set; }
        public required Role Role { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public required string Token { get; set; }
    }
}
