using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRespsitory
    {
        public TicketRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
