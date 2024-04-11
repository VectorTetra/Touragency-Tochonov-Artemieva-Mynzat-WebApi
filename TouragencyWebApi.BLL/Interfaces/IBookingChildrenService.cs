using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;


namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IBookingChildrenService
    {
        Task<IEnumerable<BookingChildrenDTO>> GetAll();
        Task<BookingChildrenDTO> GetById(long id);
        Task<IEnumerable<BookingChildrenDTO>> GetByBookingDataId(long bookingId);
        Task<IEnumerable<BookingChildrenDTO>> GetByChildrenCount(short childrenCount);
        Task<IEnumerable<BookingChildrenDTO>> GetByChildrenAge(short childrenAge);
        Task Create(BookingChildrenDTO bookingChildrenDTO);
        Task Update(BookingChildrenDTO bookingChildrenDTO);
        Task Delete(long id);
    }
}
