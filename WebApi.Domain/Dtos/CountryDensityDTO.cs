using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Dtos
{
    public class CountryDensityDTO : BaseEntity
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public decimal Area { get; set; }
        public int Population { get; set; }


        public int Populationdensity
        {
            get
            {
                return Decimal.ToInt32(Population / Area);
            }
        }
    }
}
