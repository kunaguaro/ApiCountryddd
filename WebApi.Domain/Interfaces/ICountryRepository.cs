
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interfaces
{
    public interface ICountryRepository
    {
        void Insert(Country obj);

        void Update(Country obj);

        void Delete(int id);

        Country Select(int id);

        IQueryable<Object> SelectCountries();

        List<Country> SelectAll();


    }
}
