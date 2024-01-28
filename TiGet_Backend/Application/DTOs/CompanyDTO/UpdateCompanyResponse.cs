using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CompanyDTO
{
    public class UpdateCompanyResponse
    {
        public required string Email { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Role Role { get; set; }
    }
}
