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
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        // Add other relevant properties to reflect the updated customer information
    }
}
