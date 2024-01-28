using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO
{
    public class UpdateCustomerRequest
    {
        public required string Email { get; set; }  
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
    }
}
