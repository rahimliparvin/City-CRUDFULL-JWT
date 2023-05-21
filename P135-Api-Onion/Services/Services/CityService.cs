using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.City;
using Services.DTOs.Country;
using Services.DTOs.Employee;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CityService : ICityService
    {
        private readonly ICountryRepository _countryRepo;
        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;

        public CityService(ICountryRepository countryRepo, ICityRepository cityRepo, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CityCreateDto city)
        {
            var dbCountry = await _countryRepo.GetByIdAsync(city.CountryId);

            if (dbCountry == null) throw new NullReferenceException("This name has't Country ");

            await _cityRepo.CreateAsync(_mapper.Map<City>(city));
        }


        public async Task DeleteAsync(int? id) => await _cityRepo.DeleteAsync(await _cityRepo.GetByIdAsync(id));


        public async Task<IEnumerable<CityDto>> GetAllAsync() => _mapper.Map<IEnumerable<CityDto>>(await _cityRepo.FindAllAsync());


        public async Task<CityDto> GetByIdAsync(int? id) => _mapper.Map<CityDto>(await _cityRepo.GetByIdAsync(id));

        public async Task<IEnumerable<CityDto>> SearchAsync(string? searchText)
        {
            if (searchText == null)
                return _mapper.Map<IEnumerable<CityDto>>(await _cityRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<CityDto>>(await _cityRepo.FindAllAsync(m => m.Name.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _cityRepo.SoftDeleteAsync(id);
        }

        public async Task UpdateAsync(int id, CityUpdateDto city)
        {
            if (id == null) throw new ArgumentNullException();

            var dbCity = await _cityRepo.GetByIdAsync(id);

            _mapper.Map(city, dbCity);
  

            await _cityRepo.UpdateAsync(dbCity);
        }
    }
}
