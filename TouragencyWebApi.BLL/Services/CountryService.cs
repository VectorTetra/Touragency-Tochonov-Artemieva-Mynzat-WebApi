using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DAL.Interfaces;
using AutoMapper;
using TouragencyWebApi.DAL.Entities;
using System.Numerics;

namespace TouragencyWebApi.BLL.Services
{
    public class CountryService : ICountryService
    {
        IUnitOfWork Database;

        MapperConfiguration Country_CountryDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("FlagUrl", opt => opt.MapFrom(c => c.FlagUrl))
        .ForPath(d => d.SettlementIds, opt => opt.MapFrom(c => c.Settlements.Select(b => b.Id)))
        );
        public CountryService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task Add(CountryDTO countryDTO)
        {
            var PreExistedCountry = await Database.Countries.GetByName(countryDTO.Name);
            if (PreExistedCountry.Any(em => em.Name == countryDTO.Name))
            {
                throw new ValidationException("Така країна вже існує", "");
            }
            var newCountry = new Country
            {
                Name = countryDTO.Name,
                FlagUrl= countryDTO.FlagUrl
            };

            foreach (var id in countryDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(id);
                if (settlement != null)
                {
                    newCountry.Settlements.Add(settlement);
                }
            }
            await Database.Countries.Create(newCountry);
            await Database.Save();
        }

        public async Task Update(CountryDTO countryDTO)
        {
            Country country = await Database.Countries.GetById(countryDTO.Id);
            if (country == null)
            {
                throw new ValidationException("Країну не знайдено", "");
            }
            country.Name = countryDTO.Name;
            country.FlagUrl = countryDTO.FlagUrl;
            country.Settlements.Clear();
            foreach (var id in countryDTO.SettlementIds)
            {
                var person = await Database.Settlements.GetById(id);
                if (person != null)
                {
                    country.Settlements.Add(person);
                }
            }
            Database.Countries.Update(country);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            Country country = await Database.Countries.GetById(id);
            if (country == null)
            {
                throw new ValidationException("Країну не знайдено", "");
            }
            await Database.Countries.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<CountryDTO>> GetAll()
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetAll());
        }

        public async Task<CountryDTO?> GetById(int id)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<Country, CountryDTO>(await Database.Countries.GetById(id));
        }

        public async Task<IEnumerable<CountryDTO>> GetByName(string countryName)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetByName(countryName));
        }
    }
}
