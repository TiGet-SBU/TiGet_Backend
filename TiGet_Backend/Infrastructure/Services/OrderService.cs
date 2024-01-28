using Application.DTOs.OrderDTO;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;  // Assuming an order repository

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<AddOrderResponse> AddOrder(AddOrderRequest req)
        {
            // Validate request data (e.g., check for required fields, apply business rules)

            // 1. Validate request data (example)
            if (req.CustomerId == Guid.Empty || req.TicketId == Guid.Empty ||
                string.IsNullOrEmpty(req.TicketOwnerFirstName) ||
                string.IsNullOrEmpty(req.TicketOwnerLastName) ||
                string.IsNullOrEmpty(req.NationalId))
            {
                throw new ArgumentException("Required fields are missing in the request");
            }

            // 2. Create a new order entity
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                CustomerId = req.CustomerId,
                SitPos = req.SitPos,
                TicketId = req.TicketId,
                TicketOwnerFirstName = req.TicketOwnerFirstName,
                TicketOwnerLastName = req.TicketOwnerLastName,
                NationalId = req.NationalId,
                // ... other order properties
            };

            // 3. Save the order to the repository
            await _orderRepository.AddAsync(newOrder);
            await _unitOfWork.SaveAsync();

            // 4. Create a response with relevant order details
            var response = new AddOrderResponse
            {
                CustomerId = newOrder.CustomerId,
                SitPos = newOrder.SitPos,
                TicketId = newOrder.TicketId,
                TicketOwnerFirstName = newOrder.TicketOwnerFirstName,
                TicketOwnerLastName = newOrder.TicketOwnerLastName,
                NationalId = newOrder.NationalId,
                // ... populate other response properties as needed
            };

            return response;
        }
    }
}
