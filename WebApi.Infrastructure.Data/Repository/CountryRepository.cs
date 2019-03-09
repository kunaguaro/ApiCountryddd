using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;
using WebApi.Infrastructure.Data.Context;

namespace WebApi.Infrastructure.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private SqlServerContext context;

        public CountryRepository(SqlServerContext context)
        {
            this.context = context;
        }


        public void Insert(Country country)
        {
            context.Set<Country>().Add(country);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<Country>().Remove(Select(id));
            context.SaveChanges();
        }

        public Country Select(int id)
        {
            return context.Set<Country>().Find(id);
        }

        public List<Country> SelectAll()
        {
            return context.Set<Country>().ToList();
        }

        public IQueryable<Object> SelectCountries()
        {
            return from p in context.Set<Country>()
                join b in context.Set<Country>() on p.Id equals b.Id
               
                select new
                {
                    id = p.Id,
                    name = p.Name,
                    Area = b.Area
                };
        }

        public void Update(Country country)
        {
            context.Entry(country).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

       


    }
}
