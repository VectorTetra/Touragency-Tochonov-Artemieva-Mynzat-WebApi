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
    public class BookingDataService : IBookingDataService
    {
        IUnitOfWork Database;
        public BookingDataService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration BookingData_BookingDataDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<BookingData, BookingDataDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("BookingId", opt => opt.MapFrom(c => c.BookingId))
        .ForMember("RoomNumber", opt => opt.MapFrom(c => c.RoomNumber))
        .ForMember("DateBeginPeriod", opt => opt.MapFrom(c => c.DateBeginPeriod))
        .ForMember("DateEndPeriod", opt => opt.MapFrom(c => c.DateEndPeriod))
        .ForMember("TotalPrice", opt => opt.MapFrom(c => c.TotalPrice))
        .ForMember("AdultsCount", opt => opt.MapFrom(c => c.AdultsCount))
        .ForPath(d => d.BookingChildrenIds, opt => opt.MapFrom(c => c.BookingChildren.Select(b => b.Id)))
        .ForPath(d => d.BedConfigurationId, opt => opt.MapFrom(c => c.BedConfiguration.Id))
        );
        public async Task<IEnumerable<BookingDataDTO>> GetAll()
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetAll());
        }

        public async Task<BookingDataDTO?> GetById(long id)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<BookingData, BookingDataDTO>(await Database.BookingDatas.GetById(id));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByBookingId(long bookingId)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByBookingId(bookingId));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByRoomNumber(int roomNumber)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByRoomNumber(roomNumber));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByDateDiapazon(DateTime dateBeginPeriod, DateTime dateEndPeriod)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByDateDiapazon(dateBeginPeriod, dateEndPeriod));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByTotalPriceDiapazon(int priceMinValue, int priceMaxValue)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByTotalPriceDiapazon(priceMinValue, priceMaxValue));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByAdultsCount(short adultsCount)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByAdultsCount(adultsCount));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByBookingIdRoomNumber(long bookingId, int roomNumber)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByBookingIdRoomNumber(bookingId, roomNumber));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByBookingChildrenId(long bookingChildrenId)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByBookingChildrenId(bookingChildrenId));
        }

        public async Task<IEnumerable<BookingDataDTO>> GetByBedConfigurationId(int bedConfigurationId)
        {
            var mapper = new Mapper(BookingData_BookingDataDTOMapConfig);
            return mapper.Map<IEnumerable<BookingData>, IEnumerable<BookingDataDTO>>(await Database.BookingDatas.GetByBedConfigurationId(bedConfigurationId));
        }

        public async Task Create(BookingDataDTO BookingDataDTO)
        {
            //Намагаємось визначити, чи ще не існує BookingData з таким Id
            var BusyBookingDataId = await Database.BookingDatas.GetById(BookingDataDTO.Id);
            //Якщо такий Id вже зайнято, кидаємо виключення
            if (BusyBookingDataId != null)
            {
                throw new ValidationException("Такий bookingDataId вже зайнято!", "");
            }
            //Намагаємось визначити, чи існує Booking з таким BookingId
            var PreExistedBooking = await Database.Bookings.GetById(BookingDataDTO.BookingId);
            //Якщо такого Booking не існує, кидаємо виключення
            if (PreExistedBooking == null)
            {
                throw new ValidationException("Такого бронювання із вказаним BookingId у BookingData не існує", "");
            }
            //Намагаємось визначити, чи існує BedConfiguration з таким BedConfigurationId
            var PreExistedBedConfiguration = await Database.BedConfigurations.GetById(BookingDataDTO.BedConfigurationId);
            //Якщо такого BedConfiguration не існує, кидаємо виключення
            if (PreExistedBedConfiguration == null)
            {
                throw new ValidationException("Такої конфігурації ліжка із вказаним BedConfigurationId у BookingData не існує", "");
            }

            //Створюємо новий BookingData
            var newBookingData = new BookingData
            {
                BookingId = BookingDataDTO.BookingId,
                RoomNumber = BookingDataDTO.RoomNumber,
                DateBeginPeriod = BookingDataDTO.DateBeginPeriod,
                DateEndPeriod = BookingDataDTO.DateEndPeriod,
                TotalPrice = BookingDataDTO.TotalPrice,
                AdultsCount = BookingDataDTO.AdultsCount,
                BedConfiguration = PreExistedBedConfiguration
            };
            //--------------------------------------------
            if (BookingDataDTO.BookingChildrenIds != null)
            {
                newBookingData.BookingChildren = new List<BookingChildren>();
                // Намагаємось визначити, чи існують такі BookingChildren, які вказані в BookingDataDTO
                foreach (var bookingChildrenId in BookingDataDTO.BookingChildrenIds)
                {
                    var PreExistedBookingChildren = await Database.BookingChildrens.GetById(bookingChildrenId);
                    //Якщо такі BookingChildren не існують, кидаємо виключення
                    if (PreExistedBookingChildren == null)
                    {
                        throw new ValidationException("Такого бронювання дітей із вказаним BookingChildrenId у BookingData не існує", "");
                    }
                    newBookingData.BookingChildren.Add(PreExistedBookingChildren);
                }
            }
            //--------------------------------------------
            //Додаємо новий BookingData
            await Database.BookingDatas.Create(newBookingData);
            //Зберігаємо зміни
            await Database.Save();
        }

        public async Task Update(BookingDataDTO BookingDataDTO)
        {
            //Намагаємось визначити, чи існує BookingData з таким Id
            var PreExistedBookingData = await Database.BookingDatas.GetById(BookingDataDTO.Id);
            //Якщо такого BookingData не існує, кидаємо виключення
            if (PreExistedBookingData == null)
            {
                throw new ValidationException("Такого bookingDataId в BookingData не існує!", "");
            }
            //Намагаємось визначити, чи існує Booking з таким BookingId
            var PreExistedBooking = await Database.Bookings.GetById(BookingDataDTO.BookingId);
            //Якщо такого Booking не існує, кидаємо виключення
            if (PreExistedBooking == null)
            {
                throw new ValidationException("Такого бронювання із вказаним BookingId у BookingData не існує", "");
            }
            //Намагаємось визначити, чи існує BedConfiguration з таким BedConfigurationId
            var PreExistedBedConfiguration = await Database.BedConfigurations.GetById(BookingDataDTO.BedConfigurationId);
            //Якщо такого BedConfiguration не існує, кидаємо виключення
            if (PreExistedBedConfiguration == null)
            {
                throw new ValidationException("Такої конфігурації ліжка із вказаним BedConfigurationId у BookingData не існує", "");
            }
            // -------------------------------------------------------------------------------
            PreExistedBookingData.BookingId = BookingDataDTO.BookingId;
            PreExistedBookingData.RoomNumber = BookingDataDTO.RoomNumber;
            PreExistedBookingData.DateBeginPeriod = BookingDataDTO.DateBeginPeriod;
            PreExistedBookingData.DateEndPeriod = BookingDataDTO.DateEndPeriod;
            PreExistedBookingData.TotalPrice = BookingDataDTO.TotalPrice;
            PreExistedBookingData.AdultsCount = BookingDataDTO.AdultsCount;
            PreExistedBookingData.BedConfiguration = PreExistedBedConfiguration;
            if (BookingDataDTO.BookingChildrenIds != null)
            {
                if (PreExistedBookingData.BookingChildren != null)
                {
                    PreExistedBookingData.BookingChildren.Clear();
                }
                else
                {
                    PreExistedBookingData.BookingChildren = new List<BookingChildren>();
                }
                foreach (var bookingChildrenId in BookingDataDTO.BookingChildrenIds)
                {
                    var PreExistedBookingChildren = await Database.BookingChildrens.GetById(bookingChildrenId);
                    //Якщо такі BookingChildren не існують, кидаємо виключення
                    if (PreExistedBookingChildren == null)
                    {
                        throw new ValidationException("Такого бронювання дітей із вказаним BookingChildrenId у BookingData не існує", "");
                    }
                    PreExistedBookingData.BookingChildren.Add(PreExistedBookingChildren);
                }
            }
            //--------------------------------------------
            //Оновлюємо BookingData
            Database.BookingDatas.Update(PreExistedBookingData);
            //Зберігаємо зміни
            await Database.Save();

        }

        public async Task Delete(long id)
        {
            //Намагаємось визначити, чи існує BookingData з таким Id
            var PreExistedBookingData = await Database.BookingDatas.GetById(id);
            //Якщо такого BookingData не існує, кидаємо виключення
            if (PreExistedBookingData == null)
            {
                throw new ValidationException("Такого bookingDataId в BookingData не існує!", "");
            }
            //Видаляємо BookingData
            await Database.BookingDatas.Delete(id);
            //Зберігаємо зміни
            await Database.Save();
        }

    }
}
