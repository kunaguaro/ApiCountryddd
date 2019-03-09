using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Dtos;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;
using WebApi.Service.MappingDtos;

namespace WebApi.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository iCountryRepository;
        private readonly IMapper mapper;

        public CountryService(ICountryRepository iCountryRepository, IMapper mapper)
        {
            this.iCountryRepository = iCountryRepository;
            this.mapper = mapper;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            iCountryRepository.Delete(id);
        }

        public Country Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return iCountryRepository.Select(id);
        }

        public IQueryable<Object> Get() => iCountryRepository.SelectCountries();

        public List<Country> GetAll()
        {
          return  iCountryRepository.SelectAll();
        }

        public IEnumerable<CountryDensityDTO> GetAllCountriesDensity()
        {
            List<Country> model = new List<Country>();
            model = iCountryRepository.SelectAll();
            var attachments = mapper.Map<List<CountryDensityDTO>>(model); // Mapper.Map<IEnumerable<Country>, List<CountryDensityDTO>>(model);
            return attachments;
        }


        public Country Post<V>(Country country) where V : AbstractValidator<Country>
        {
            Validate(country, Activator.CreateInstance<V>());

            iCountryRepository.Insert(country);
            return country;
        }

        public Country Put<V>(Country country) where V : AbstractValidator<Country>
        {
            Validate(country, Activator.CreateInstance<V>());

            iCountryRepository.Update(country);
            return country;
        }

       

        private void Validate(Country obj, AbstractValidator<Country> validator)
        {
            if (obj == null)
                throw new Exception("records not found!");

            validator.ValidateAndThrow(obj);
        }
    }
}
