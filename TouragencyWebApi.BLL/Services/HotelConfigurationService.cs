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
    public class HotelConfigurationService : IHotelConfigurationService
    {
        IUnitOfWork Database;
        public HotelConfigurationService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration HotelConfiguration_HotelConfigurationDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<HotelConfiguration, HotelConfigurationDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("IsAllowChildren", opt => opt.MapFrom(c => c.IsAllowChildren))
        .ForMember("IsAllowPets", opt => opt.MapFrom(c => c.IsAllowPets))
        .ForMember("CompassSide", opt => opt.MapFrom(c => c.CompassSide))
        .ForMember("WindowView", opt => opt.MapFrom(c => c.WindowView))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(b => b.Id)))
        );
        public async Task<IEnumerable<HotelConfigurationDTO>> GetAll()
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetAll());
        }

        public async Task<IEnumerable<HotelConfigurationDTO>> Get200Last()
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.Get200Last());
        }
        public async Task<HotelConfigurationDTO?> GetById(int id)
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<HotelConfiguration, HotelConfigurationDTO>(await Database.HotelConfigurations.GetById(id));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByHotelId(hotelId));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByCompassSideSubstring(string compassSideSubstring)
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByCompassSideSubstring(compassSideSubstring));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByWindowViewSubstring(string WindowViewSubstring) 
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByWindowViewSubstring(WindowViewSubstring));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByIsAllowChildren(bool isAllowChildren) 
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByIsAllowChildren(isAllowChildren));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByIsAllowPets(bool isAllowPets) 
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByIsAllowPets(isAllowPets));
        }
        public async Task<IEnumerable<HotelConfigurationDTO>> GetByCompositeSearch(int? hotelId, string? compassSideSubstring, string? WindowViewSubstring, bool? isAllowChildren, bool? isAllowPets)
        {
            var mapper = new Mapper(HotelConfiguration_HotelConfigurationDTOMapConfig);
            return mapper.Map<IEnumerable<HotelConfiguration>, IEnumerable<HotelConfigurationDTO>>(await Database.HotelConfigurations.GetByCompositeSearch(hotelId, compassSideSubstring, WindowViewSubstring, isAllowChildren, isAllowPets));
        }
        public async Task<HotelConfigurationDTO> Create(HotelConfigurationDTO hotelConfigurationDTO) 
        {
            var PreExistedHotelConfig = await Database.HotelConfigurations.GetById(hotelConfigurationDTO.Id);
            if (PreExistedHotelConfig != null)
            {
                throw new ValidationException($"Конфігурація готелю з таким id вже існує! (id : {hotelConfigurationDTO.Id})", "");
            }
            var hotels = new List<Hotel>();
            foreach (var id in hotelConfigurationDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готель з таким hotelConfigurationDTO.HotelId не знайдено! (hotelId: {id})", "");
                }
                hotels.Add(hotel);
            }
            var newHotelConfiguration = new HotelConfiguration
            {
                IsAllowChildren = hotelConfigurationDTO.IsAllowChildren,
                IsAllowPets = hotelConfigurationDTO.IsAllowPets,
                CompassSide = hotelConfigurationDTO.CompassSide,
                WindowView = hotelConfigurationDTO.WindowView,
                Hotels = hotels
            };
            await Database.HotelConfigurations.Create(newHotelConfiguration);
            await Database.Save();
            hotelConfigurationDTO.Id = newHotelConfiguration.Id;
            return hotelConfigurationDTO;

        }
        public async Task<HotelConfigurationDTO> Update(HotelConfigurationDTO hotelConfigurationDTO) 
        {
            var hotelConfiguration = await Database.HotelConfigurations.GetById(hotelConfigurationDTO.Id);
            if (hotelConfiguration == null)
            {
                throw new ValidationException($"Конфігурації готелю з таким id не існує! (id : {hotelConfigurationDTO.Id})", "");
            }
            hotelConfiguration.IsAllowChildren = hotelConfigurationDTO.IsAllowChildren;
            hotelConfiguration.IsAllowPets = hotelConfigurationDTO.IsAllowPets;
            hotelConfiguration.CompassSide = hotelConfigurationDTO.CompassSide;
            hotelConfiguration.WindowView = hotelConfigurationDTO.WindowView;
            hotelConfiguration.Hotels.Clear();
            foreach (var id in hotelConfigurationDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готель з таким hotelConfigurationDTO.HotelId не знайдено! (hotelId: {id})", "");
                }
                hotelConfiguration.Hotels.Add(hotel);
            }
            Database.HotelConfigurations.Update(hotelConfiguration);
            await Database.Save();
            return hotelConfigurationDTO;
        }
        public async Task<HotelConfigurationDTO> Delete(int id)
        {
            var hotelConfiguration = await Database.HotelConfigurations.GetById(id);
            if (hotelConfiguration == null)
            {
                throw new ValidationException($"Конфігурації готелю з таким id не існує! (id : {id})", "");
            }
            var dto = await GetById(id);
            await Database.HotelConfigurations.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
