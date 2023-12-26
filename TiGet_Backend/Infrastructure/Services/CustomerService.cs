using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Application.DTOs;
using Application.DTOs.CustomerDTO.Auth;
using Application.DTOs.TicketDTO;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _jwtSecret = "";
    private readonly int _jwtExpirationInMinutes = 3600; // Token expiration time

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private string GenerateRandomKey(int lengthInBits)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] keyBytes = new byte[lengthInBits / 8];
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }
    }


    public async Task<CustomerRegisterResponse> Register(CustomerRegisterRequest req)
    {
        if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
        {
            throw new ArgumentException("Email and password are required");
        }

        // Check if email is unique
        if (await _unitOfWork.CustomerRepository.GetByConditionAsync(e => e.Email == req.Email) != null)
        {
            throw new InvalidOperationException("Email is already registered");
        }

        // Create a new concrete user entity
        var newUser = new Customer
        {
            Id = Guid.NewGuid(),
            Role = Role.Customer,
            Email = req.Email,
            PasswordHash = HashPassword(req.Password),
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
        if (user != null && VerifyPassword(req.Password, user.PasswordHash))
        {
            // Return a JWT token
            string token =  GenerateJwtToken(user);
            var response = new CustomerLoginResponse
            {
                Email = user.Email,
                Role = user.Role,
                Name = user.LastName,
                Token = token
            };

            return response;
        }

        throw new InvalidOperationException("Invalid login credentials");
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

    // ----------
    private string GenerateJwtToken(User user)
    {
        var _jwtSecret = GenerateRandomKey(256);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string HashPassword(string password)
    {
        // Hash the password using BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
    {
        // Verify the password using BCrypt
        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
    }

}
