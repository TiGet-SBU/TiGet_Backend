using Application.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICityRepository CityRepositoty { get; set; }

        public ICompanyRepository CompanyRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; } 

        public IOrderRepository OrderRepository { get; set; }

        public IStationRepository StationRepository { get; set; }

        public ITicketRespsitory TicketRespsitory { get; set; }

        public IVehicleRepository VehicleRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CityRepositoty = new CityRepository(context);
            CompanyRepository = new CompanyRepository(context);
            CustomerRepository = new CustomerRepository(context);
            OrderRepository = new OrderRepository(context);
            StationRepository = new StationRepository(context);
            TicketRespsitory = new TicketRepository(context);
            VehicleRepository = new VehicleRepository(context);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
