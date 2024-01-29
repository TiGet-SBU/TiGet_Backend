using System;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Threading.Tasks;
using Application.DTOs.TicketDTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace TestProject
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task AddTicket_ValidRequest_ReturnsTicketAddResponse()
        {
            // Arrange 
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.TicketRespsitory.GetByConditionAsync(It.IsAny<Expression<Func<Ticket, bool>>>()))
                .ReturnsAsync((Ticket)null);

            var ticketService = new TicketService(unitOfWorkMock.Object);
            var request = new TicketAddRequest
            {
                VehicleId = Guid.NewGuid(),
                TimeToGo = DateTime.Now,
                Price = 10.99,
                // Set other properties in the request 
            };

            // Act 
            var response = await ticketService.AddTicket(request);

            // Assert 
            Assert.NotNull(response);
            Assert.Equal(request.TimeToGo, response.TimeToGo);
            Assert.Equal(request.Price, response.Price);
            // Assert other properties in the response 
        }
        /*
        [Fact]
        public async Task AddTicket_DuplicateTicket_ThrowsInvalidOperationException()
        {
            // Arrange 
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.TicketRespsitory.GetByConditionAsync(It.IsAny<Expression<Func<Ticket, bool>>>()))
                .ReturnsAsync(new Ticket());

            var ticketService = new TicketService(unitOfWorkMock.Object);
            var request = new TicketAddRequest
            {
                VehicleId = Guid.NewGuid(),
                TimeToGo = DateTime.Now,
                Price = 10.99,
                // Set other properties in the request 
            };

            // Act & Assert 
            await Assert.ThrowsAsync<InvalidOperationException>(() => ticketService.AddTicket(request));
        }

        [Fact]
        public async Task GetTicketById_ExistingTicket_ReturnsTicketGetByIdResponse()
        {
            // Arrange 
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var existingTicket = new Ticket {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                TimeToGo = request.TimeToGo,
                Price = request.Price,
                VehicleId = request.VehicleId,
                CompanyId = (Guid)request.CompanyId,
                SourceId = request.SourceId,
                DestinationId = request.DestinationId,
                Source = request.Source,
                Destination = request.Destination
            };
            unitOfWorkMock.Setup(uow => uow.TicketRespsitory.GetByConditionAsync(It.IsAny<Expression<Func<Ticket, bool>>>()))
                .ReturnsAsync(existingTicket);

            var ticketService = new TicketService(unitOfWorkMock.Object);
            var request = new TicketGetByIdRequest
            {
                Id = existingTicket.Id,
            };

            // Act 
            var response = await ticketService.GetTicketById(request);

            // Assert 
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(existingTicket, response.Ticket);
        }

        [Fact]
        public async Task GetTicketById_NonExistingTicket_ReturnsTicketGetByIdResponseWithErrorMessage()
        {
            // Arrange 
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.TicketRespsitory.GetByConditionAsync(It.IsAny<Expression<Func<Ticket, bool>>>()))
                .ReturnsAsync((Ticket)null);

            var ticketService = new TicketService(unitOfWorkMock.Object);
            var request = new TicketGetByIdRequest
            {
                Id = Guid.NewGuid(),
            };

            // Act 
            var response = await ticketService.GetTicketById(request);

            // Assert 
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Ticket not found", response.ErrorMessage);
        }
   */
    }
}
        // Add more test methods for other methods in TicketServic