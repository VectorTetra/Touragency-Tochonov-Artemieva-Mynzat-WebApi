using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IBookingChildrenRepository
    {
        Task<IEnumerable<BookingChildren>> GetAll();
        Task<BookingChildren?> GetById(long id);
        Task<IEnumerable<BookingChildren>> GetByBookingDataId(long bookingId);
        Task<IEnumerable<BookingChildren>> GetByChildrenCount(short childrenCount);
        Task<IEnumerable<BookingChildren>> GetByChildrenAge(short childrenAge);
        Task Create(BookingChildren bookingChildren);
        void Update(BookingChildren bookingChildren);
        Task Delete(long id);
    }
}
