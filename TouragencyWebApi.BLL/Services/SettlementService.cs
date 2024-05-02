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
        .ForPath(d => d.TourNameIds, opt => opt.MapFrom(c => c.TourNames.Select(b => b.Id)))
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
        public async Task<SettlementDTO> Add(SettlementDTO settlementDTO)
        {
            var mapper = new Mapper(Country_CountryDTOMapConfig);
            var PreExistedSettlement = await Database.Settlements.GetByName(settlementDTO.Name);
            if (PreExistedSettlement.Any(em => em.Name == settlementDTO.Name && em.Country.Id == settlementDTO.CountryId))
            {
                throw new ValidationException($"Такий населений пункт із вказаною назвою вже існує в цій країні! (settlementDTO.Name : {settlementDTO.Name}, countryId : {settlementDTO.CountryId})", "") ;
            }
            var newSettlement = new Settlement
            {
                Name = settlementDTO.Name
            };
            var coun = await Database.Countries.GetById(settlementDTO.CountryId);
            if (coun == null)
            {
                throw new ValidationException($"Країни із вказаним countryId не знайдено! (countryId : {settlementDTO.CountryId})", "");
            }
            newSettlement.Country = coun;
            newSettlement.TourNames = new List<TourName>();
            newSettlement.Hotels = new List<Hotel>();
            foreach(var id in settlementDTO.TourNameIds)
            {
                var tourName = await Database.TourNames.GetById(id);
                if (tourName == null)
                {
                    throw new ValidationException($"Назви туру із вказаним id не знайдено! (tourNameId : {id})", "");
                }
                newSettlement.TourNames.Add(tourName);
            }
            foreach(var id in settlementDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готелю із вказаним id не знайдено! (hotelId : {id})", "");
                }
                newSettlement.Hotels.Add(hotel);
            }

            await Database.Settlements.Create(newSettlement);
            await Database.Save();
            settlementDTO.Id = newSettlement.Id;
            return settlementDTO;
        }
        public async Task<SettlementDTO> Update(SettlementDTO settlementDTO)
        {
            
            Settlement settlement = await Database.Settlements.GetById(settlementDTO.Id);
            if (settlement == null)
            {
                throw new ValidationException($"Такий населений пункт не знайдено! (Id : {settlementDTO.Id}) ", "");
            }
            settlement.Name = settlementDTO.Name;
            var coun = await Database.Countries.GetById(settlementDTO.CountryId);
            if (coun == null)
            {
                throw new ValidationException($"Країни із вказаним countryId не знайдено! (countryId : {settlementDTO.CountryId})", "");
            }
            settlement.Country = coun;
            settlement.TourNames.Clear();
            settlement.Hotels.Clear();
            foreach (var id in settlementDTO.TourNameIds)
            {
                var tourName = await Database.TourNames.GetById(id);
                if (tourName == null)
                {
                    throw new ValidationException($"Назви туру із вказаним id не знайдено! (tourNameId : {id})", "");
                }
                settlement.TourNames.Add(tourName);
            }
            foreach (var id in settlementDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готелю із вказаним id не знайдено! (hotelId : {id})", "");
                }
                settlement.Hotels.Add(hotel);
            }

            Database.Settlements.Update(settlement);
            await Database.Save();
            return settlementDTO;
        }
        public async Task<SettlementDTO> Delete(int id)
        {
            Settlement settlement = await Database.Settlements.GetById(id);
            if (settlement == null)
            {
                throw new ValidationException($"Такий населений пункт не знайдено! (Id : {id}) ", "");
            }
            var dto = await GetById(id);
            await Database.Settlements.Delete(id);
            await Database.Save();
            return dto;
        }
        public async Task<IEnumerable<SettlementDTO>> GetAll()
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetAll());
        }

        public async Task<IEnumerable<SettlementDTO>> Get200Last()
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.Get200Last());
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
        
        public async Task<IEnumerable<SettlementDTO>> GetByTourNameId(int tourNameId)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByTourNameId(tourNameId));
        }

        public async Task<IEnumerable<SettlementDTO>> GetByTourName(string tourName)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByTourName(tourName));
        }

        public async Task<SettlementDTO?> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<Settlement, SettlementDTO>(await Database.Settlements.GetByHotelId(hotelId));
        }

        public async Task<IEnumerable<SettlementDTO>> GetByCompositeSearch(string? name, string? countryName, int? countryId, int? tourNameId, string? tourName)
        {
            var mapper = new Mapper(Settlement_SettlementDTOMapConfig);
            return mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementDTO>>(await Database.Settlements.GetByCompositeSearch(name, countryName, countryId, tourNameId, tourName));
        }
    }
}
