﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDTO
{
    public class AddOrderResponse
    {
        public required Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public required int SitPos { get; set; }
        public required Guid TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public required string TicketOwnerFirstName { get; set; }
        public required string TicketOwnerLastName { get; set; }
        public required string NationalId { get; set; }

    }
}
