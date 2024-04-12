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
    public class HotelServiceService: IHotelServiceService
    {
        IUnitOfWork Database;
        public HotelServiceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration HotelService_HotelServiceDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TouragencyWebApi.DAL.Entities.HotelService, HotelServiceDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.HotelServiceTypeId, opt => opt.MapFrom(c => c.HotelServiceType.Id))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(b => b.Id)))
        );

        public async Task<IEnumerable<HotelServiceDTO>> GetAll()
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyWebApi.DAL.Entities.HotelService>, IEnumerable<HotelServiceDTO>>(await Database.HotelServices.GetAll());
        }

        public async Task<HotelServiceDTO?> GetById(int id)
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<TouragencyWebApi.DAL.Entities.HotelService, HotelServiceDTO>(await Database.HotelServices.GetById(id));
        }

        public async Task<IEnumerable<HotelServiceDTO>> GetByHotelId(int hotelId)
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyWebApi.DAL.Entities.HotelService>, IEnumerable<HotelServiceDTO>>(await Database.HotelServices.GetByHotelId(hotelId));
        }

        public async Task<IEnumerable<HotelServiceDTO>> GetByHotelServiceTypeId(int hotelServiceTypeId)
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyWebApi.DAL.Entities.HotelService>, IEnumerable<HotelServiceDTO>>(await Database.HotelServices.GetByHotelServiceTypeId(hotelServiceTypeId));
        }

        public async Task<IEnumerable<HotelServiceDTO>> GetByNameSubstring(string nameSubstring)
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyWebApi.DAL.Entities.HotelService>, IEnumerable<HotelServiceDTO>>(await Database.HotelServices.GetByNameSubstring(nameSubstring));
        }

        public async Task<IEnumerable<HotelServiceDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            IMapper mapper = new Mapper(HotelService_HotelServiceDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyWebApi.DAL.Entities.HotelService>, IEnumerable<HotelServiceDTO>>(await Database.HotelServices.GetByDescriptionSubstring(descriptionSubstring));
        }

        public async Task Create(HotelServiceDTO hotelServiceDTO)
        {
            var PreExistedHotelService = await Database.HotelServices.GetById(hotelServiceDTO.Id);
            if (PreExistedHotelService != null)
            {
                throw new ValidationException($"HotelService з таким Id вже існує! (id : {hotelServiceDTO.Id})", "") ;
            }
            var PreExistedHotelServiceByName = await Database.HotelServices.GetByNameSubstring(hotelServiceDTO.Name);
            foreach (var item in PreExistedHotelServiceByName)
            {
                if (item.Name == hotelServiceDTO.Name)
                {
                    throw new ValidationException($"HotelService з таким іменем вже існує! (Name : {hotelServiceDTO.Name})", "");
                }
            }
            var HotelCollection = new List<Hotel>();
            foreach (var item in hotelServiceDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(item);
                if (hotel == null)
                {
                    throw new ValidationException($"Hotel з таким hotelServiceDTO.HotelId не існує! (id : {item})", "");
                }
                HotelCollection.Add(hotel);
            }
            var HotelService = new TouragencyWebApi.DAL.Entities.HotelService
            {
                Id = hotelServiceDTO.Id,
                Name = hotelServiceDTO.Name,
                Description = hotelServiceDTO.Description,
                HotelServiceType = await Database.HotelServiceTypes.GetById(hotelServiceDTO.HotelServiceTypeId),
                Hotels = HotelCollection
            };
            await Database.HotelServices.Create(HotelService);
            await Database.Save();
        }

        public async Task Update(HotelServiceDTO hotelServiceDTO)
        {
            var HotelService = await Database.HotelServices.GetById(hotelServiceDTO.Id);
            if (HotelService == null)
            {
                throw new ValidationException($"HotelService з таким Id не існує! (id : {hotelServiceDTO.Id})", "");
            }
            var PreExistedHotelServiceByName = await Database.HotelServices.GetByNameSubstring(hotelServiceDTO.Name);
            foreach (var item in PreExistedHotelServiceByName)
            {
                if (item.Name == hotelServiceDTO.Name && item.Id != hotelServiceDTO.Id)
                {
                    throw new ValidationException($"HotelService з таким іменем вже існує! (Name : {hotelServiceDTO.Name})", "");
                }
            }
            HotelService.Hotels.Clear();
            foreach (var item in hotelServiceDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(item);
                if (hotel == null)
                {
                    throw new ValidationException($"Hotel з таким hotelServiceDTO.HotelId не існує! (id : {item})", "");
                }
                HotelService.Hotels.Add(hotel);
            }
            var hotelServType = await Database.HotelServiceTypes.GetById(hotelServiceDTO.HotelServiceTypeId);
            if (hotelServType == null)
            {
                throw new ValidationException($"HotelServiceType з таким Id не існує! (id : {hotelServiceDTO.HotelServiceTypeId})", "");
            }

            HotelService.Name = hotelServiceDTO.Name;
            HotelService.Description = hotelServiceDTO.Description;
            HotelService.HotelServiceType = hotelServType;
            Database.HotelServices.Update(HotelService);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            var hotelService = await Database.HotelServices.GetById(id);
            if (hotelService == null)
            {
                throw new ValidationException($"HotelService з таким Id не існує! (id : {id})", "");
            }
            await Database.HotelServices.Delete(id);
            await Database.Save();
        }
    }
}
