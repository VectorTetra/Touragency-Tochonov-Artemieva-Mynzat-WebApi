using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDTO>> GetAll();
        Task<HotelDTO?> GetById(int id);
        Task<IEnumerable<HotelDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<HotelDTO>> GetByStars(int[] selectedStarsRatings);
        Task<IEnumerable<HotelDTO>> GetByHotelConfigurationId(int hotelConfigurationId);
        Task<IEnumerable<HotelDTO>> GetByBedConfigurationId(int bedConfigurationId);
        Task<IEnumerable<HotelDTO>> GetBySettlementId(int settlementId);
        Task<IEnumerable<HotelDTO>> GetByTourId(long tourId);
        Task<IEnumerable<HotelDTO>> GetByBookingId(long bookingId);
        Task<IEnumerable<HotelDTO>> GetByHotelServiceId(int settlementId);
        Task Create(HotelDTO hotel);
        Task Update(HotelDTO hotel);
        Task Delete(int id);
    }
}
