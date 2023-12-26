using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(UserRegisterDTO registerDTO);
    Task<string> LoginAsync(UserLoginDTO loginDTO);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly ICustomerRepository _userRepository; // Assume you have IUserRepository
    private readonly string _jwtSecret = "your_jwt_secret_key"; // Replace with a secure key
    private readonly int _jwtExpirationInMinutes = 1440; // Token expiration time

    public AuthenticationService(ICustomerRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> RegisterAsync(UserRegisterDTO registerDTO)
    {
        // Validate input
        if (string.IsNullOrEmpty(registerDTO.Email) || string.IsNullOrEmpty(registerDTO.Password))
        {
            throw new ArgumentException("Email and password are required");
        }

        // Check if email is unique
        if (await _userRepository.GetByConditionAsync(e => e.Email == registerDTO.Email) != null)
        {
            throw new InvalidOperationException("Email is already registered");
        }

        // Create a new concrete user entity
        var newUser = new Customer
        {
            Id = Guid.NewGuid(),
            Role = Role.Customer,
            Email = registerDTO.Email,
            PasswordHash =  HashPassword(registerDTO.Password),
            PhoneNumber= registerDTO.PhoneNumber,
            CreatedDate = DateTime.Now,
        };

        // Save the user entity to the repository
        await _userRepository.AddAsync(newUser);

        // Return a JWT token
        return GenerateJwtToken(newUser);
    }

    public async Task<string> LoginAsync(UserLoginDTO loginDTO)
    {
        // Validate input
        if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
        {
            throw new ArgumentException("Email and password are required");
        }

        // Find the user by email
        var user = await _userRepository.GetByConditionAsync(e => e.Email == loginDTO.Email);

        // Check if the user exists and the password is correct
        if (user != null && VerifyPassword(loginDTO.Password, user.PasswordHash))
        {
            // Return a JWT token
            return GenerateJwtToken(user);
        }

        throw new InvalidOperationException("Invalid login credentials");
    }

    private string GenerateJwtToken(User user)
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
