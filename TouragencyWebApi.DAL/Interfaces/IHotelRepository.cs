using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAll();
        Task<Hotel?> GetById(int id);
        Task<IEnumerable<Hotel>> GetByCountry(string country);
        Task<IEnumerable<Hotel>> GetByCity(string city);
        Task<IEnumerable<Hotel>> GetByStars(short stars);
        Task<IEnumerable<Hotel>> GetByPrice(decimal price);
        Task Create(Hotel hotel);
        void Update(Hotel hotel);
        Task Delete(int id);
    }
}
