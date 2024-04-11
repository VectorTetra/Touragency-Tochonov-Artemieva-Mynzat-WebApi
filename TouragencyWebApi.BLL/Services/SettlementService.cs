using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;

namespace TouragencyWebApi.BLL.Services
{
    public class SettlementService : ISettlementService
    {
        IUnitOfWork Database;

        MapperConfiguration Settlement_SettlementDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Settlement, SettlementDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("CountryId", opt => opt.MapFrom(c => c.Country.Id))
        .ForPath(d => d.ToursIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(b => b.Id)))
        );
        MapperConfiguration Country_CountryDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("FlagUrl", opt => opt.MapFrom(c => c.FlagUrl))
        .ForPath(d => d.SettlementIds, opt => opt.MapFrom(c => c.Settlements.Select(b => b.Id)))
        );
        public SettlementService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task Add(SettlementDTO settlementDTO)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            var PreExistedSettlement = await Database.Settlements.GetByName(settlementDTO.Name);
            if (PreExistedSettlement.Any(em => em.Name == settlementDTO.Name))
            {
                throw new ValidationException("Такий населений пункт вже існує", "");
            }
            var newSettlement = new Settlement
            {
                Name = settlementDTO.Name
            };
            newSettlement.Country = await Database.Countries.GetById(settlementDTO.CountryId);

            await Database.Settlements.Create(newSettlement);
            await Database.Save();
        }
        public async Task Update(SettlementDTO settlementDTO)
        {
            
            Settlement settlement = await Database.Settlements.GetById(settlementDTO.Id);
            if (settlement == null)
            {
                throw new ValidationException("Такий населений пункт не знайдено", "");
            }
            settlement.Name = settlementDTO.Name;
            settlement.Country = await Database.Countries.GetById(settlementDTO.CountryId);

            Database.Settlements.Update(settlement);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            Settlement settlement = await Database.Settlements.GetById(id);
            if (settlement == null)
            {
                throw new ValidationException("Країну не знайдено", "");
            }
            await Database.Settlements.Delete(id);
            await Database.Save();
        }
        public async Task<IEnumerable<SettlementDTO>> GetAll()
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetAll());
        }

        public async Task<SettlementDTO?> GetById(int id)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<Settlement, SettlementDTO>(await Database.Settlements.GetById(id));
        }

        public async Task<IEnumerable<SettlementDTO>> GetByName(string settlementName)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByName(settlementName));
        }
        public async Task<IEnumerable<SettlementDTO>> GetByCountryName(string countryName)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByCountryName(countryName));
        }

        public async Task<IEnumerable<SettlementDTO>> GetByCountryId(int countryId)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByCountryId(countryId));
        }
        
        public async Task<IEnumerable<SettlementDTO>> GetByTourId(long tourId) 
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByTourId(tourId));
        }
        public async Task<SettlementDTO?> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<Settlement, SettlementDTO>(await Database.Settlements.GetByHotelId(hotelId));
        }
    }
}
