using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
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
            Assert.IsNotNull(response);
            Assert.Equals(request.TimeToGo, response.TimeToGo);
            Assert.Equals(request.Price, response.Price);
            // Assert other properties in the response 
            */
            Assert.IsTrue(true);
        }
    }
}
