﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDTO
{
    public record TicketGetAllRequest
    {
        public int first = 0;
        public int last = int.MaxValue;
        public Expression<Func<Ticket, bool>>? condition = null;
    }
}
