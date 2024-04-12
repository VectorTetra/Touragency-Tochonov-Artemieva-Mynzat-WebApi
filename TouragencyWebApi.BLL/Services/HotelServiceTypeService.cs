using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class HotelServiceTypeService: IHotelServiceTypeService
    {
        IUnitOfWork Database;
        public HotelServiceTypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration HotelServiceType_HotelServiceTypeDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<HotelServiceType, HotelServiceTypeDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.HotelServiceIds, opt => opt.MapFrom(c => c.HotelServices.Select(b => b.Id)))
        );
        public async Task<IEnumerable<HotelServiceTypeDTO>> GetAll()
        {
            IMapper mapper = new Mapper(HotelServiceType_HotelServiceTypeDTOMapConfig);
            return mapper.Map<IEnumerable<HotelServiceType>, IEnumerable<HotelServiceTypeDTO>>(await Database.HotelServiceTypes.GetAll());
        }

        public async Task<HotelServiceTypeDTO?> GetById(int id)
        {
            IMapper mapper = new Mapper(HotelServiceType_HotelServiceTypeDTOMapConfig);
            return mapper.Map<HotelServiceType, HotelServiceTypeDTO>(await Database.HotelServiceTypes.GetById(id));
        }

        public async Task<IEnumerable<HotelServiceTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            IMapper mapper = new Mapper(HotelServiceType_HotelServiceTypeDTOMapConfig);
            return mapper.Map<IEnumerable<HotelServiceType>, IEnumerable<HotelServiceTypeDTO>>(await Database.HotelServiceTypes.GetByDescriptionSubstring(descriptionSubstring));
        }

        public async Task<IEnumerable<HotelServiceTypeDTO>> GetByHotelServiceId(int hotelServiceId)
        {
            IMapper mapper = new Mapper(HotelServiceType_HotelServiceTypeDTOMapConfig);
            return mapper.Map<IEnumerable<HotelServiceType>, IEnumerable<HotelServiceTypeDTO>>(await Database.HotelServiceTypes.GetByHotelServiceId(hotelServiceId));
        }

        public async Task Create(HotelServiceTypeDTO hotelServiceTypeDTO)
        {
            var PreExistedHotelServiceType = await Database.HotelServiceTypes.GetById(hotelServiceTypeDTO.Id);
            if (PreExistedHotelServiceType != null)
            {
                throw new ValidationException($"HotelServiceType з таким Id вже існує! (id : {hotelServiceTypeDTO.Id})", "") ;
            }
            var PreExistedHotelServiceTypeByDesr = await Database.HotelServiceTypes.GetByDescriptionSubstring(hotelServiceTypeDTO.Description);
            foreach (var item in PreExistedHotelServiceTypeByDesr)
            {
                if (item.Description == hotelServiceTypeDTO.Description)
                {
                    throw new ValidationException($"HotelServiceType з таким Description вже існує! (Description : {hotelServiceTypeDTO.Description})", "");
                }
            }

            HotelServiceType hotelServiceType = new HotelServiceType
            {
                Id = hotelServiceTypeDTO.Id,
                Description = hotelServiceTypeDTO.Description,
                HotelServices = new List<TouragencyWebApi.DAL.Entities.HotelService>()
            };
            foreach (var item in hotelServiceTypeDTO.HotelServiceIds)
            {
                var hotelService = await Database.HotelServices.GetById(item);
                if (hotelService == null)
                {
                    throw new ValidationException($"HotelService з таким Id не існує! (id : {item})", "");
                }
                hotelServiceType.HotelServices.Add(hotelService);
            }
            await Database.HotelServiceTypes.Create(hotelServiceType);
            await Database.Save();
        }

        public async Task Update(HotelServiceTypeDTO hotelServiceTypeDTO)
        {
            var hotelServiceType = await Database.HotelServiceTypes.GetById(hotelServiceTypeDTO.Id);
            if (hotelServiceType == null)
            {
                throw new ValidationException($"Такий HotelServiceType з вказаним Id не знайдено! (id : {hotelServiceTypeDTO.Id})", "");
            }
            var PreExistedHotelServiceTypeByDesr = await Database.HotelServiceTypes.GetByDescriptionSubstring(hotelServiceTypeDTO.Description);
            foreach (var item in PreExistedHotelServiceTypeByDesr)
            {
                if (item.Description == hotelServiceTypeDTO.Description && item.Id != hotelServiceTypeDTO.Id)
                {
                    throw new ValidationException($"HotelServiceType з таким Description вже існує! (Description : {hotelServiceTypeDTO.Description})", "");
                }
            }
            hotelServiceType.Description = hotelServiceTypeDTO.Description;
            hotelServiceType.HotelServices.Clear();
            foreach (var item in hotelServiceTypeDTO.HotelServiceIds)
            {
                var hotelService = await Database.HotelServices.GetById(item);
                if (hotelService == null)
                {
                    throw new ValidationException($"HotelService з таким Id не існує! (id : {item})", "");
                }
                hotelServiceType.HotelServices.Add(hotelService);
            }
            Database.HotelServiceTypes.Update(hotelServiceType);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            var hotelServiceType = await Database.HotelServiceTypes.GetById(id);
            if (hotelServiceType == null)
            {
                throw new ValidationException($"Такий HotelServiceType з вказаним Id не знайдено! (id : {id})", "");
            }
                await Database.HotelServiceTypes.Delete(id);

        }
    }
}
