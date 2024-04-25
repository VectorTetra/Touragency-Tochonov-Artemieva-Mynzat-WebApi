using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Interfaces;
using AutoMapper;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.Infrastructure;

namespace TouragencyWebApi.BLL.Services
{
    public class TransportTypeService : ITransportTypeService
    {
        IUnitOfWork Database;
        public TransportTypeService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration TransportType_TransportTypeDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TransportType, TransportTypeDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(t => t.Id)))
        );
        public async Task<IEnumerable<TransportTypeDTO>> GetAll()
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetAll();
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<TransportTypeDTO?> GetById(int id)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetById(id);
            var transportTypeDTO = mapper.Map<TransportType, TransportTypeDTO>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<IEnumerable<TransportTypeDTO>> GetByNameSubstring(string nameSubstring)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetByNameSubstring(nameSubstring);
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<IEnumerable<TransportTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetByDescriptionSubstring(descriptionSubstring);
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<IEnumerable<TransportTypeDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetByTourId(tourId);
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<IEnumerable<TransportTypeDTO>> GetByTourName(string tourname)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetByTourName(tourname);
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<IEnumerable<TransportTypeDTO>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring, long? tourId, string? tourname)
        {
            var mapper = new Mapper(TransportType_TransportTypeDTOMapConfig);
            var transportTypeCollection = await Database.TransportTypes.GetByCompositeSearch(nameSubstring, descriptionSubstring, tourId, tourname);
            var transportTypeDTO = mapper.Map<IEnumerable<TransportType>, IEnumerable<TransportTypeDTO>>(transportTypeCollection);
            return transportTypeDTO;
        }

        public async Task<TransportTypeDTO> Create(TransportTypeDTO transportType)
        {
            //Намагаємось визначити, чи ще не існує тур з таким tourId
            var BusyTrTypeId = await Database.TransportTypes.GetById(transportType.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyTrTypeId != null)
            {
                throw new ValidationException("Такий transportTypeId вже зайнято!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            var TourCollection = new List<Tour>();
            //-----------------------------------------------------------------------------------------------------
            foreach (var tourId in transportType.TourIds)
            {
                var tour = await Database.Tours.GetById(tourId);
                if (tour == null)
                {
                    throw new ValidationException("Тур з таким tourId не знайдено!", "");
                }
                TourCollection.Add(tour);
            }
            //-----------------------------------------------------------------------------------------------------
            var newTrType = new TransportType
            {
                Name = transportType.Name,
                Description = transportType.Description,
                Tours = TourCollection
            };

            await Database.TransportTypes.Create(newTrType);
            await Database.Save();
            transportType.Id = newTrType.Id;
            return transportType;
        }

        public async Task<TransportTypeDTO> Update(TransportTypeDTO transportType)
        {
            //Намагаємось визначити, чи ще не існує тур з таким tourId
            var TrType = await Database.TransportTypes.GetById(transportType.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (TrType == null)
            {
                throw new ValidationException("Такий transportTypeId не знайдено!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            TrType.Tours.Clear();
            //-----------------------------------------------------------------------------------------------------
            foreach (var tourId in transportType.TourIds)
            {
                var tour = await Database.Tours.GetById(tourId);
                if (tour == null)
                {
                    throw new ValidationException("Тур з таким tourId не знайдено!", "");
                }
                TrType.Tours.Add(tour);
            }
            //-----------------------------------------------------------------------------------------------------
            TrType.Name = transportType.Name;
            TrType.Description = transportType.Description;
            Database.TransportTypes.Update(TrType);
            await Database.Save();
            return transportType;
        }

        public async Task<TransportTypeDTO> Delete(int id)
        {
            var TrType = await Database.TransportTypes.GetById(id);
            if (TrType == null)
            {
                throw new ValidationException("Такий transportTypeId не знайдено!", "");
            }
            var dto = await GetById(id);
            await Database.TransportTypes.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
