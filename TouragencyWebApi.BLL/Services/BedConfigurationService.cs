using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using AutoMapper;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.Infrastructure;

namespace TouragencyWebApi.BLL.Services
{
    public class BedConfigurationService : IBedConfigurationService
    {
        IUnitOfWork Database;
        public BedConfigurationService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration BedConfiguration_BedConfigurationDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<BedConfiguration, BedConfigurationDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Capacity", opt => opt.MapFrom(c => c.Capacity))
        .ForMember("Label", opt => opt.MapFrom(c => c.Label))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.BookingDataIds, opt => opt.MapFrom(c => c.BookingDatas.Select(t => t.Id)))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(t => t.Id)))
        );
        public async Task Create(BedConfigurationDTO bedConfigurationDTO)
        {
            //Намагаємось визначити, чи ще не існує BedConfigurations з таким Id
            var BusyBedConfigurationId = await Database.BedConfigurations.GetById(bedConfigurationDTO.Id);
            //Якщо такий Id вже зайнято, кидаємо виключення
            if (BusyBedConfigurationId != null)
            {
                throw new ValidationException("Такий bedConfigurationId вже зайнято!", "");
            }
            //=======================================================================================================
            // Намагаємось визначити, чи ще не існує BedConfigurations з таким Label (Навіть якщо порядок міток різний)
            // Наприклад, якщо перевіряються масиви ["SGL","DBL","SFA"] і ["DBL","SFA","SGL"], то повинен викинутися ValidationException
            var bedsInDTO = bedConfigurationDTO.Label.Split('+');
            var existingBedConfigurations = await Database.BedConfigurations.GetAll();
            foreach (var item in existingBedConfigurations)
            {
                var sequenceInItem = item.Label.Split('+');
                if (bedsInDTO.OrderBy(a => a).SequenceEqual(sequenceInItem.OrderBy(a => a)))
                {
                    throw new ValidationException("Такий bedConfiguration із вказаним Label вже зайнято!", "");
                }
            }
            //=======================================================================================================
            var Hotels = new List<Hotel>();
            if (bedConfigurationDTO.HotelIds != null)
            {
                foreach (var item in bedConfigurationDTO.HotelIds)
                {
                    var hotel = await Database.Hotels.GetById(item);
                    if (hotel == null)
                    {
                        throw new ValidationException("Hotel з таким Id не знайдено!", "");
                    }
                    Hotels.Add(hotel);
                }
            }
            //=======================================================================================================
            var BookingDatas = new List<BookingData>();
            if (bedConfigurationDTO.BookingDataIds != null)
            {
                foreach (var item in bedConfigurationDTO.BookingDataIds)
                {
                    var bookingData = await Database.BookingDatas.GetById(item);
                    if (bookingData == null)
                    {
                        throw new ValidationException("BookingData з таким Id не знайдено!", "");
                    }
                    BookingDatas.Add(bookingData);
                }
            }
            //=======================================================================================================
            BedConfiguration bedConfiguration = new BedConfiguration
            {
                Id = bedConfigurationDTO.Id,
                Capacity = bedConfigurationDTO.Capacity,
                Label = bedConfigurationDTO.Label,
                Description = bedConfigurationDTO.Description,
                Hotels = Hotels,
                BookingDatas = BookingDatas
            };
            await Database.BedConfigurations.Create(bedConfiguration);
            await Database.Save();
        }
        public async Task Update(BedConfigurationDTO bedConfigurationDTO)
        { //Намагаємось визначити, чи існує BedConfigurations з таким Id
            var BedConfiguration = await Database.BedConfigurations.GetById(bedConfigurationDTO.Id);
            //Якщо такий Id не знайдено, кидаємо виключення
            if (BedConfiguration == null)
            {
                throw new ValidationException("Такий bedConfigurationId не знайдено!", "");
            }
            //=======================================================================================================
            // Намагаємось визначити, чи ще не існує BedConfigurations з таким Label (Навіть якщо порядок міток різний)
            // Наприклад, якщо перевіряються масиви ["SGL","DBL","SFA"] і ["DBL","SFA","SGL"], то повинен викинутися ValidationException
            // (крім випадку, коли id конфігурацій збігаються)
            var bedsInDTO = bedConfigurationDTO.Label.Split('+');
            var existingBedConfigurations = await Database.BedConfigurations.GetAll();
            foreach (var item in existingBedConfigurations)
            {
                var sequenceInItem = item.Label.Split('+');
                if (bedsInDTO.OrderBy(a => a).SequenceEqual(sequenceInItem.OrderBy(a => a)) && bedConfigurationDTO.Id != item.Id)
                {
                    throw new ValidationException("Такий bedConfiguration із вказаним Label вже зайнято!", "");
                }
            }
            //=======================================================================================================
            if (bedConfigurationDTO.HotelIds != null)
            {
                if (BedConfiguration.Hotels == null)
                {
                    BedConfiguration.Hotels = new List<Hotel>();
                }
                BedConfiguration.Hotels.Clear();
                foreach (var item in bedConfigurationDTO.HotelIds)
                {
                    var hotel = await Database.Hotels.GetById(item);
                    if (hotel == null)
                    {
                        throw new ValidationException("Hotel з таким Id не знайдено!", "");
                    }
                    BedConfiguration.Hotels.Add(hotel);
                }
            }
            //=======================================================================================================
            if (bedConfigurationDTO.BookingDataIds != null)
            {
                if (BedConfiguration.BookingDatas == null)
                {
                    BedConfiguration.BookingDatas = new List<BookingData>();
                }
                BedConfiguration.BookingDatas.Clear();
                foreach (var item in bedConfigurationDTO.HotelIds)
                {
                    var hotel = await Database.BookingDatas.GetById(item);
                    if (hotel == null)
                    {
                        throw new ValidationException("Hotel з таким Id не знайдено!", "");
                    }
                    BedConfiguration.BookingDatas.Add(hotel);
                }
            }
            //=======================================================================================================
            BedConfiguration.Id = bedConfigurationDTO.Id;
            BedConfiguration.Capacity = bedConfigurationDTO.Capacity;
            BedConfiguration.Label = bedConfigurationDTO.Label;
            BedConfiguration.Description = bedConfigurationDTO.Description;
            //=======================================================================================================
            Database.BedConfigurations.Update(BedConfiguration);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            var BedConfiguration = await Database.BedConfigurations.GetById(id);
            if (BedConfiguration == null)
            {
                throw new ValidationException("Такий bedConfigurationId не знайдено!", "");
            }
            await Database.BedConfigurations.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<BedConfigurationDTO>> GetAll()
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetAll();
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }

        public async Task<BedConfigurationDTO?> GetById(int id)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetById(id);
            var bedConfigurationDTO = mapper.Map<BedConfiguration, BedConfigurationDTO>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }

        public async Task<IEnumerable<BedConfigurationDTO>> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetByHotelId(hotelId);
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }
        public async Task<IEnumerable<BedConfigurationDTO>> GetByBookingDataId(long bookingDataId)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetByBookingDataId(bookingDataId);
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }
        public async Task<IEnumerable<BedConfigurationDTO>> GetByCapacity(short capacity)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetByCapacity(capacity);
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }
        public async Task<IEnumerable<BedConfigurationDTO>> GetByLabelSubstring(string labelSubstring)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetByLabelSubstring(labelSubstring);
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }
        public async Task<IEnumerable<BedConfigurationDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var mapper = new Mapper(BedConfiguration_BedConfigurationDTOMapConfig);
            var bedConfigurationCollection = await Database.BedConfigurations.GetByDescriptionSubstring(descriptionSubstring);
            var bedConfigurationDTO = mapper.Map<IEnumerable<BedConfiguration>, IEnumerable<BedConfigurationDTO>>(bedConfigurationCollection);
            return bedConfigurationDTO;
        }
    }
}
