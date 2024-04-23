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
        Task<IEnumerable<HotelImageDTO>> Get200Last();
        Task<HotelImageDTO?> GetById(long id);
        Task<IEnumerable<HotelImageDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task<HotelImageDTO> Create(HotelImageDTO hotelImage);
        Task<HotelImageDTO> Update(HotelImageDTO hotelImage);
        Task<HotelImageDTO> Delete(long id);
    }
}
