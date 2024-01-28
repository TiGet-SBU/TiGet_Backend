using Application.DTOs.CityDTO;
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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CityAddResponse> AddCity (CityAddRequest req)
        {
            if (req == null || req.CityName == null || req.Province == null) 
                throw new ArgumentException("null object or argument");

            if (await _unitOfWork.CityRepositoty
                .GetByConditionAsync(e => e.CityName == req.CityName && e.Province == req.Province) != null)
                throw new InvalidOperationException("duplicate entity");

            City newCity = new City 
                { 
                    CityName = req.CityName,
                    Province = req.Province,
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                };

            await _unitOfWork.CityRepositoty.AddAsync(newCity);
            await _unitOfWork.SaveAsync();

            CityAddResponse response = new CityAddResponse
            {
                Province = newCity.Province.ToLower(),
                CityName = newCity.CityName.ToLower(),
                CreatedTime = newCity.CreatedDate,
            };

            return response;
        }

        public async Task<bool> DeleteCity(Guid id)
        {
            City? entity = await _unitOfWork.CityRepositoty.GetByConditionAsync(e => e.Id == id);
            if (entity != null)
                return false;

            _unitOfWork.CityRepositoty.Delete(entity!);

            return true;
        }


    }
}
