using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO;
using Application.DTOs.CustomerDTO.Auth;
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
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<RegisterResponse> Register(RegisterRequest req)
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
            if (await _unitOfWork.CompanyRepository.GetByConditionAsync(e => e.Email == req.Email.ToLower()) != null
                || await _unitOfWork.CustomerRepository.GetByConditionAsync(e => e.Email == req.Email.ToLower()) != null)
            {
                throw new InvalidOperationException("Email is already registered");
            }

            // Create a new concrete user entity
            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                Address = "address",
                CreatedDate = DateTime.Now,
                Description = "description",
                Email = req.Email,
                Name = "company name",
                Role = req.Role,
                PasswordHash = AuthService.HashPassword(req.Password),
            };

            // Save the user entity to the repository
            await _unitOfWork.CompanyRepository.AddAsync(newCompany);
            await _unitOfWork.SaveAsync();

            // Return a response with user details
            var response = new RegisterResponse
            {
                Email = newCompany.Email,
                Role = newCompany.Role,
            };

            return response;
        }


        public async Task<CompanyLoginResponse> Login(CompanyLoginRequest req)
        {
            if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                throw new ArgumentException("Email and password are required");
            }

            var company = await _unitOfWork.CompanyRepository.GetByConditionAsync(e => e.Email == req.Email);

            // Check if the user exists and the password is correct
            if (company != null && AuthService.VerifyPassword(req.Password, company.PasswordHash))
            {
                // Return a JWT token
                string token = AuthService.GenerateJwtToken(company);
                var response = new CompanyLoginResponse
                {
                    Email = company.Email,
                    Role = company.Role,
                    Name = company.Name,
                    PhoneNumber = company.PhoneNumber,
                    Token = token,
                    Address = company.Address,
                    Description = company.Description,
                };

                return response;
            }

            throw new InvalidOperationException("Invalid login credentials");
        }

        public async Task<UpdateCompanyResponse> UpdateCompany(UpdateCompanyRequest req)
        {
            if (req.Email == null)
            {
                throw new ArgumentException("Email should not be empty");
            }

            var existingCompany = await _unitOfWork.CompanyRepository.GetByConditionAsync(e => e.Email == req.Email.ToLower());
            if (existingCompany == null)
            {
                throw new InvalidOperationException("Company not found");
            }

            existingCompany.Name = req.Name;
            existingCompany.PhoneNumber = req.PhoneNumber;

            _unitOfWork.CompanyRepository.Update(existingCompany);
            await _unitOfWork.SaveAsync();

            // Return a response with updated customer details
            var response = new UpdateCompanyResponse
            {
                Email = existingCompany.Email,
                Role = existingCompany.Role,
                Name = existingCompany.Name,
                PhoneNumber = existingCompany.PhoneNumber
            };

            return response;
        }

        

    }
}
