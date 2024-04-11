using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<Booking?> GetById(long id);
        Task<IEnumerable<Booking>> GetByTourId(long tourId);
        Task<IEnumerable<Booking>> GetByClientId(int clientId);
        Task<IEnumerable<Booking>> GetByHotelId(int hotelId);
        Task<IEnumerable<Booking>> GetByBookingDataId(int bookingDataId);
        Task<IEnumerable<Booking>> GetByBookingChildrenId(int bookingChildrenId);
        Task Create(Booking booking);
        void Update(Booking booking);
        Task Delete(long id);
    }
}
