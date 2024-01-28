using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO
{
    public class UpdateCustomerResponse
    {
        public required string Email { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; 
        public Role Role { get; set; }
        // Add other relevant properties to reflect the updated customer information
    }
}
