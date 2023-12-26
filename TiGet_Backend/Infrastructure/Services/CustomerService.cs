using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.CustomerDTO.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _jwtSecret = "your_jwt_secret_key"; // Replace with a secure key
    private readonly int _jwtExpirationInMinutes = 1440; // Token expiration time

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
        // Return a JWT token
        var response = new CustomerRegisterResponse { 
            Email = newUser.Email,
            Role = newUser.Role,         
        };
        //return GenerateJwtToken(newUser);
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
            //return GenerateJwtToken(user);
            var response = new CustomerLoginResponse { 
                Email = user.Email ,
                Role = user.Role,
            };
        }

        throw new InvalidOperationException("Invalid login credentials");
    }


    private string GenerateJwtToken(Customer user)
    {
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
        // Implement a secure password hashing mechanism (e.g., using BCrypt or Identity Framework)
        // For simplicity, we'll use a basic example
        return password; // Replace with actual hashing logic
    }

    private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
    {
        // Implement password verification logic (e.g., using BCrypt or Identity Framework)
        // For simplicity, we'll use a basic example
        return enteredPassword == storedPasswordHash;
    }
}
