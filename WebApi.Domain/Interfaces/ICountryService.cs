using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Dtos;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interfaces
{
    public interface ICountryService
    {
        Country Post<V>(Country obj) where V : AbstractValidator<Country>;

        Country Put<V>(Country obj) where V : AbstractValidator<Country>;

        void Delete(int id);

        Country Get(int id);

        IQueryable<Object> Get();

        List<Country> GetAll();

        IEnumerable<CountryDensityDTO> GetAllCountriesDensity();

    }
}
