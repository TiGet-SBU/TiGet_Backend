using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CompanyDTO
{
    public class CompanyLoginResponse
    {
        public required string Email { get; set; }
        public required Role Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get;set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required string Token { get; set; }
    }
}
