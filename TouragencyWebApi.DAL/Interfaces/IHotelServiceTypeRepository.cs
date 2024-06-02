using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelServiceTypeRepository
    {
        Task<IEnumerable<HotelServiceType>> GetAll();
        Task<IEnumerable<HotelServiceType>> Get200Last();
        Task<HotelServiceType?> GetById(int id);
        Task<IEnumerable<HotelServiceType>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelServiceType>> GetByHotelServiceId(int hotelServiceId);
        Task Create(HotelServiceType hotelServiceType);
        void Update(HotelServiceType hotelServiceType);
        Task Delete(int id);
    }
}
