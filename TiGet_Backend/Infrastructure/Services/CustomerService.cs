using Application.DTOs.CustomerDTO;
using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.TicketDTO;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<CustomerRegisterResponse> Register(CustomerRegisterRequest req)
    {
        if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
        {
            throw new ArgumentException("Email and password are required");
        }

        if (req.Password != req.PasswordRepeat)
        {
            throw new ArgumentException("Password and Repeat of Password is not the same");
        }

        // Check if email is unique
        if (await _unitOfWork.CustomerRepository.GetByConditionAsync(e => e.Email == req.Email.ToLower()) != null)
        {
            throw new InvalidOperationException("Email is already registered");
        }

        // Create a new concrete user entity
        var newUser = new Customer
        {
            Id = Guid.NewGuid(),
            Role = req.Role,
            Email = req.Email.ToLower(),
            PasswordHash = AuthService.HashPassword(req.Password),
            CreatedDate = DateTime.Now,
        };

        // Save the user entity to the repository
        await _unitOfWork.CustomerRepository.AddAsync(newUser);
        await _unitOfWork.SaveAsync();

        // Return a response with user details
        var response = new CustomerRegisterResponse
        {
            Email = newUser.Email,
            Role = newUser.Role,
        };

        return response;
    }


    public async Task<CustomerLoginResponse> Login(CustomerLoginRequest req)
    {
        // Validate input
        if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
        {
            throw new ArgumentException("Email and password are required");
        }

        // Find the user by email
        var user = await _unitOfWork.CustomerRepository.GetByConditionAsync(e => e.Email == req.Email);

        // Check if the user exists and the password is correct
        if (user != null && AuthService.VerifyPassword(req.Password, user.PasswordHash))
        {
            // Return a JWT token
            string token =  AuthService.GenerateJwtToken(user);
            var response = new CustomerLoginResponse
            {
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber, 
                Token = token
            };

            return response;
        }

        throw new InvalidOperationException("Invalid login credentials");
    }

    public async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest req)
    {
        if (req.Email == null)
        {
            throw new ArgumentException("Email should not be empty");
        }

        var existingCustomer = await _unitOfWork.CustomerRepository.GetByConditionAsync(e => e.Email == req.Email);
        if (existingCustomer == null)
        {
            throw new InvalidOperationException("Customer not found");
        }

        existingCustomer.FirstName = req.FirstName;
        existingCustomer.LastName = req.LastName;
        existingCustomer.PhoneNumber = req.PhoneNumber;

        _unitOfWork.CustomerRepository.Update(existingCustomer);
        await _unitOfWork.SaveAsync();

        // Return a response with updated customer details
        var response = new UpdateCustomerResponse
        {
            Email = existingCustomer.Email,
            Role = existingCustomer.Role,
            FirstName = existingCustomer.FirstName,
            LastName = existingCustomer.LastName,
            PhoneNumber = existingCustomer.PhoneNumber
        };

        return response;
    }


    
    public async Task<IEnumerable<TicketGetResponse>> GetAllTickets(TicketGetAllRequest req)
    {
        var ans = await _unitOfWork.TicketRespsitory.GetAllAsync(req.first, req.last, req.condition,
            "Vehicle", "Company", "Source", "Destination");

        IEnumerable<TicketGetResponse> response = ans.Select(e => new TicketGetResponse
        {
            Source = e.Source,
            Destination = e.Destination,
            Company = e.Company,
            Price = e.Price,
            TimeToGo = e.TimeToGo,
            Vehicle = e.Vehicle,
        });

        return response;
    }
    
}
