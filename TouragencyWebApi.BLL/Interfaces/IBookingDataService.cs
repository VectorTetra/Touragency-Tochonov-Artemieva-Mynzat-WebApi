using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IBookingDataService
    {
        Task<IEnumerable<BookingDataDTO>> GetAll();
        Task<BookingDataDTO?> GetById(long id);
        Task<IEnumerable<BookingDataDTO>> GetByBookingId(long bookingId);
        Task<IEnumerable<BookingDataDTO>> GetByRoomNumber(int roomNumber);
        Task<IEnumerable<BookingDataDTO>> GetByDateDiapazon(DateTime dateBeginPeriod, DateTime dateEndPeriod);
        Task<IEnumerable<BookingDataDTO>> GetByTotalPriceDiapazon(int priceMinValue, int priceMaxValue);
        Task<IEnumerable<BookingDataDTO>> GetByAdultsCount(short adultsCount);
        Task<IEnumerable<BookingDataDTO>> GetByBookingIdRoomNumber(long bookingId, int roomNumber);
        Task<IEnumerable<BookingDataDTO>> GetByBookingChildrenId(long bookingChildrenId);
        Task<IEnumerable<BookingDataDTO>> GetByBedConfigurationId(int bedConfigurationId);
        Task Create(BookingDataDTO BookingDataDTO);
        Task Update(BookingDataDTO BookingDataDTO);
        Task Delete(long id);
    }
}
