using AutoMapper;
using WebApi.Domain.Dtos;
using WebApi.Domain.Entities;

namespace WebApi.Service.MappingDtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Country, CountryDensityDTO>();

        }
    }
}
