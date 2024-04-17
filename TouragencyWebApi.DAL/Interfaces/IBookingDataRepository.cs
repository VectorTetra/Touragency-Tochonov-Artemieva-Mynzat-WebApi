using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IBookingDataRepository
    {
        Task<IEnumerable<BookingData>> GetAll();
        Task<BookingData?> GetById(long id);
        Task<IEnumerable<BookingData>> GetByBookingId(long bookingId);
        Task<IEnumerable<BookingData>> GetByRoomNumber(int roomNumber);
        Task<IEnumerable<BookingData>> GetByDateDiapazon(DateTime dateBeginPeriod, DateTime dateEndPeriod);
        Task<IEnumerable<BookingData>> GetByTotalPriceDiapazon(int priceMinValue, int priceMaxValue);
        Task<IEnumerable<BookingData>> GetByAdultsCount(short adultsCount);
        Task<IEnumerable<BookingData>> GetByBookingIdRoomNumber(long bookingId, int roomNumber);
        Task<IEnumerable<BookingData>> GetByBookingChildrenId(long bookingChildrenId);
        Task<IEnumerable<BookingData>> GetByBedConfigurationId(int bedConfigurationId);
        Task Create(BookingData bookingData);
        void Update(BookingData bookingData);
        Task Delete(long id);
    }
}
