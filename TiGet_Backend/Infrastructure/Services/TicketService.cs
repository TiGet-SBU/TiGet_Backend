using Application.DTOs.TicketDTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TicketAddResponse> AddTicket(TicketAddRequest request)
        {
            // Validation
            if (request.VehicleId == Guid.Empty)
            {
                throw new ArgumentException("Vehicle ID is required");
            }
            // Add other validation rules as needed

            // Check for unique constraints (if applicable)
            if (await _unitOfWork.TicketRespsitory.GetByConditionAsync(e => e.VehicleId == request.VehicleId && e.TimeToGo == request.TimeToGo) != null)
            {
                throw new InvalidOperationException("A ticket with the same vehicle and time already exists");
            }

            // Create a new ticket entity
            var newTicket = new Ticket
            {
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

            // Save the ticket entity to the repository
            await _unitOfWork.TicketRespsitory.AddAsync(newTicket);
            await _unitOfWork.SaveAsync();

            // Return a response with ticket details
            var response = new TicketAddResponse
            {
                Id = newTicket.Id,
                TimeToGo = newTicket.TimeToGo,
                Price = newTicket.Price,
                // Add other relevant properties to the response
            };

            return response;
        }

        public async Task<TicketGetByIdResponse> GetTicketById(TicketGetByIdRequest request)
        {
            try
            {
                
                Ticket ticket = await _unitOfWork.TicketRespsitory.GetByConditionAsync(e => e.Id==request.Id);

                if (ticket != null)
                {
                    // Return the ticket if found
                    return new TicketGetByIdResponse
                    {
                        Ticket = ticket,
                        Success = true
                    };
                }
                else
                {
                    // Return an error message if the ticket is not found
                    return new TicketGetByIdResponse
                    {
                        Success = false,
                        ErrorMessage = "Ticket not found"
                    };
                }
            }
            catch (Exception ex)
            {
                return new TicketGetByIdResponse
                {
                    Success = false,
                    ErrorMessage = $"An error occurred: {ex.Message}"
                };
            }
        }
        public async Task<TicketGetAllResponse> GetAllTickets(TicketGetAllRequest request)
        {
            try
            {
                
                List<Ticket> tickets = (List<Ticket>)await _unitOfWork.TicketRespsitory.GetAllAsync();

                // Return the list of tickets
                return new TicketGetAllResponse
                {
                    Tickets = tickets,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new TicketGetAllResponse
                {
                    Success = false,
                    ErrorMessage = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<TicketUpdateResponse> UpdateTicket(TicketUpdateRequest request)
        {
            // Validation
            if (request.Id.Equals(0))
            {
                throw new ArgumentException("Ticket ID is required for update");
            }
            // Add other validation rules as needed

            // Fetch the existing ticket
            var existingTicket = await _unitOfWork.TicketRespsitory.GetByConditionAsync(e => e.Id == request.Id); // Cast to Guid for consistency
            if (existingTicket == null)
            {
                throw new InvalidOperationException($"Ticket with ID {request.Id} not found");
            }

            // Update entity properties based on request
            if (request.TimeToGo != null) existingTicket.TimeToGo = request.TimeToGo;
            if (request.Price != null) existingTicket.Price = request.Price; // Cast to decimal for consistency
            if (request.VehicleId != null) existingTicket.VehicleId = request.VehicleId;
            if (request.CompanyId != null) existingTicket.CompanyId = request.CompanyId;
            if (request.SourceId != null) existingTicket.SourceId = request.SourceId;
            if (request.DestinationId != null) existingTicket.DestinationId = request.DestinationId;

            _unitOfWork.TicketRespsitory.Update(existingTicket);
            await _unitOfWork.SaveAsync();

            // Return a response with updated ticket details
            var response = new TicketUpdateResponse
            {
                Id = existingTicket.Id,
                TimeToGo = existingTicket.TimeToGo,
                Price = existingTicket.Price,
                // Add other relevant properties to the response
            };

            return response;
        }


        public async Task<TicketDeleteResponse> DeleteTicket(TicketDeleteRequest request)
        {
            // Validation
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Ticket ID is required for deletion");
            }

            // Fetch the ticket to delete
            var ticketToDelete = await _unitOfWork.TicketRespsitory.GetByConditionAsync(e => e.Id == request.Id);
            if (ticketToDelete == null)
            {
                throw new InvalidOperationException($"Ticket with ID {request.Id} not found");
            }

            // Delete the ticket
            _unitOfWork.TicketRespsitory.Delete(ticketToDelete);
            await _unitOfWork.SaveAsync();

            // Return a success response
            return new TicketDeleteResponse
            {
                Success = true
            };
        }
    }
}


