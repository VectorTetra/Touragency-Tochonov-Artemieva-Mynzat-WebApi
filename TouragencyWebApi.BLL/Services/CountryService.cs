using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Services
{
    public class CountryService : ICountriesRepository
    {
        IUnitOfWork Database;

        MapperConfiguration Country_CountryDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("FlagUrl", opt => opt.MapFrom(c => c.FlagUrl))
        );

        public CountryService(IUnitOfWork uow)
        {
            Database = uow;
        }
    }
}
