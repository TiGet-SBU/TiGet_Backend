using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO
{
    public class UpdateCustomerRequest
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }  // Only include if password update is allowed
                                               // Add other properties as needed, such as Address, Phone, etc.
    }
}
