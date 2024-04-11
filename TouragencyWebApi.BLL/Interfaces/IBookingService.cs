using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAll();
        Task<BookingDTO> GetById(long id);
        Task<IEnumerable<BookingDTO>> GetByClientId(int clientId);
        Task<IEnumerable<BookingDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<BookingDTO>> GetByTourId(long tourId); 
        Task<IEnumerable<BookingDTO>> GetByBookingDataId(long bookingDataId);
        Task Create(BookingDTO bookingDTO);
        Task Update(BookingDTO bookingDTO);
        Task Delete(long id);
    }
}
