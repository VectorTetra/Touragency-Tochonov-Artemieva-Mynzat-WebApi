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
        .ForPath(d => d.ContinentId, opt => opt.MapFrom(c => c.Continent.Id))
        .ForPath(d => d.ContinentName, opt => opt.MapFrom(c => c.Continent.Name))
        );
        public CountryService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<CountryDTO> Add(CountryDTO countryDTO)
        {
            var PreExistedCountry = await Database.Countries.GetByName(countryDTO.Name);
            if (PreExistedCountry.Any(em => em.Name == countryDTO.Name))
            {
                throw new ValidationException($"Така країна вже існує (countryDTO.Name : {countryDTO.Name})", "");
            }
            var newCountry = new Country
            {
                Name = countryDTO.Name,
                FlagUrl= countryDTO.FlagUrl
            };

            foreach (var id in countryDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(id);
                if (settlement == null)
                {
                    throw new ValidationException($"Населений пункт із вказаним id не знайдено! (settlementId : {id})", "");
                }
                newCountry.Settlements.Add(settlement);
            }
            var continent = await Database.Continents.GetById(countryDTO.ContinentId);
            if (continent == null)
            {
                throw new ValidationException($"Континент із вказаним id не знайдено! (continentId : {countryDTO.ContinentId})", "");
            }
            newCountry.Continent = continent;
            await Database.Countries.Create(newCountry);
            await Database.Save();

            countryDTO.Id = newCountry.Id;
            return countryDTO;
        }

        public async Task<CountryDTO> Update(CountryDTO countryDTO)
        {
            Country country = await Database.Countries.GetById(countryDTO.Id);
            if (country == null)
            {
                throw new ValidationException($"Країну з вказаним Id не знайдено (countryDTO.Id : {countryDTO.Id})", "");
            }
            country.Name = countryDTO.Name;
            country.FlagUrl = countryDTO.FlagUrl;
            var continent = await Database.Continents.GetById(countryDTO.ContinentId);
            if (continent == null)
            {
                throw new ValidationException($"Континент із вказаним id не знайдено! (continentId : {countryDTO.ContinentId})", "");
            }
            country.Continent = continent;
            country.Settlements.Clear();
            foreach (var id in countryDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(id);
                if (settlement == null)
                {
                    throw new ValidationException($"Населений пункт із вказаним id не знайдено! (settlementId : {id})", "");
                }
                country.Settlements.Add(settlement);
            }
            Database.Countries.Update(country);
            await Database.Save();
            return countryDTO;
        }

        public async Task<CountryDTO> Delete(int id)
        {
            Country country = await Database.Countries.GetById(id);
            if (country == null)
            {
                throw new ValidationException($"Країну з вказаним Id не знайдено (id : {id})", "");
            }
            var countryDTO = await GetById(id);
            await Database.Countries.Delete(id);
            await Database.Save();
            return countryDTO;
        }

        public async Task<IEnumerable<CountryDTO>> GetAll()
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetAll());
        }

        public async Task<IEnumerable<CountryDTO>> Get200Last()
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.Get200Last());
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

        public async Task<IEnumerable<CountryDTO>> GetByContinentName(string continentName)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetByContinentName(continentName));
        }

        public async Task<IEnumerable<CountryDTO>> GetByContinentId(int continentId)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetByContinentId(continentId));
        }

        public async Task<IEnumerable<CountryDTO>> GetByCompositeSearch(string? name, string? continentName, int? continentId)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(await Database.Countries.GetByCompositeSearch(name, continentName, continentId));
        }
    }
}
