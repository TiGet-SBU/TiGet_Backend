using Application.DTOs.CityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICityService
    {
        public Task<CityAddResponse> AddCity (CityAddRequest request);

        public Task<bool> DeleteCity(Guid id);
    }
}
