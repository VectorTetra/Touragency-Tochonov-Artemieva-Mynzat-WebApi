using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelImageService
    {
        Task<IEnumerable<HotelImageDTO>> GetAll();
        Task<HotelImageDTO?> GetById(long id);
        Task<IEnumerable<HotelImageDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task Create(HotelImageDTO hotelImage);
        Task Update(HotelImageDTO hotelImage);
        Task Delete(long id);
    }
}
