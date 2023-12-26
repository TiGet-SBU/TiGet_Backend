using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();


        // repositories
        public ICityRepository CityRepositoty { get; }
        public ICompanyRepository CompanyRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IStationRepository StationRepository { get; }
        public ITicketRespsitory TicketRespsitory { get; }
        public IVehicleRepository VehicleRepository { get; }
    }
}
