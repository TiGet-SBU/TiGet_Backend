﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {


        // do we really need? 
        // Task<bool> Exists(Func<User, bool> predicate);
    }
}
