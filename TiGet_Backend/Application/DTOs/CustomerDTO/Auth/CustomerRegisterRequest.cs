﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTO.Auth
{
    public record CustomerRegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
