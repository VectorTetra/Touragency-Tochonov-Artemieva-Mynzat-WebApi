using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelImageRepository
    {
        Task<IEnumerable<HotelImage>> GetAll();
        Task<IEnumerable<HotelImage>> Get200Last();
        Task<HotelImage?> GetById(long id);
        Task<IEnumerable<HotelImage>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelImage>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task Create(HotelImage hotelImage);
        void Update(HotelImage hotelImage);
        Task Delete(long id);
    }
}
