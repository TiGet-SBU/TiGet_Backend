using Application.DTOs.CompanyDTO;
using Application.DTOs.CustomerDTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICompanyService
    {
        public Task<RegisterResponse> Register(RegisterRequest req);

        public Task<CompanyLoginResponse> Login(CompanyLoginRequest req);

    }
}
