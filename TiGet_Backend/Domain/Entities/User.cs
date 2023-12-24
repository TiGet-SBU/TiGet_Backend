﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract record User
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
