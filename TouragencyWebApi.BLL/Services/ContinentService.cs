using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class ContinentService: IContinentService
    {
        IUnitOfWork Database;

        MapperConfiguration Continent_ContinentDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Continent, ContinentDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForPath(d => d.CountryIds, opt => opt.MapFrom(c => c.Countries.Select(b => b.Id)))
        );
        public ContinentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<ContinentDTO> Create(ContinentDTO continentDTO)
        {
            var PreExistedContinent = await Database.Continents.GetByName(continentDTO.Name);
            if (PreExistedContinent.Any(em => em.Name == continentDTO.Name))
            {
                throw new ValidationException($"Такий континент вже існує (continentDTO.Name : {continentDTO.Name})", "");
            }
            var newContinent = new Continent
            {
                Name = continentDTO.Name,
                Countries = new List<Country>()
            };
            
            await Database.Continents.Create(newContinent);
            await Database.Save();

            continentDTO.Id = newContinent.Id;
            return continentDTO;
        }

        public async Task<ContinentDTO> Delete(int id)
        {
            var continent = await Database.Continents.GetById(id);
            if (continent == null)
            {
                throw new ValidationException($"Континент із вказаним id не знайдено! (continentId : {id})", "");
            }
            var dto = await GetById(id);
            await Database.Continents.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<ContinentDTO>> GetAll()
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<IEnumerable<Continent>, IEnumerable<ContinentDTO>>(await Database.Continents.GetAll());
        }

        public async Task<ContinentDTO> GetById(int id)
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<Continent, ContinentDTO>(await Database.Continents.GetById(id));
        }

        public async Task<ContinentDTO> Update(ContinentDTO continentDTO)
        {
            Continent continent = await Database.Continents.GetById(continentDTO.Id);
            if (continent == null)
            {
                throw new ValidationException($"Континент із вказаним Id не знайдено (continentDTO.Id : {continentDTO.Id})", "");
            }
            continent.Name = continentDTO.Name;
            continent.Countries.Clear();
            foreach (var id in continentDTO.CountryIds)
            {
                var country = await Database.Countries.GetById(id);
                if (country == null)
                {
                    throw new ValidationException($"Країну із вказаним id не знайдено! (countryId : {id})", "");
                }
                continent.Countries.Add(country);
            }
            Database.Continents.Update(continent);
            await Database.Save();
            return continentDTO;
        }

        public async Task<IEnumerable<ContinentDTO>> GetByCompositeSearch(string? name, string? countryName, int? countryId)
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<IEnumerable<Continent>, IEnumerable<ContinentDTO>>(await Database.Continents.GetByCompositeSearch(name, countryName, countryId));
        }

        public async Task<IEnumerable<ContinentDTO>> GetByCountryId(int countryId)
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<IEnumerable<Continent>, IEnumerable<ContinentDTO>>(await Database.Continents.GetByCountryId(countryId));
        }

        public async Task<IEnumerable<ContinentDTO>> GetByCountryName(string countryName)
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<IEnumerable<Continent>, IEnumerable<ContinentDTO>>(await Database.Continents.GetByCountryName(countryName));
        }

        public async Task<IEnumerable<ContinentDTO>> GetByName(string name)
        {
            var mapper = new Mapper(Continent_ContinentDTOMapConfig);
            return mapper.Map<IEnumerable<Continent>, IEnumerable<ContinentDTO>>(await Database.Continents.GetByName(name));
        }
    }
}
