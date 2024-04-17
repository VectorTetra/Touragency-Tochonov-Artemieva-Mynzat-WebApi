using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelConfigurationRepository
    {
        Task<IEnumerable<HotelConfiguration>> GetAll();
        Task<HotelConfiguration?> GetById(int id);
        Task<IEnumerable<HotelConfiguration>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelConfiguration>> GetByCompassSideSubstring(string compassSideSubstring);
        Task<IEnumerable<HotelConfiguration>> GetByWindowViewSubstring(string WindowViewSubstring);
        Task<IEnumerable<HotelConfiguration>> GetByIsAllowChildren(bool isAllowChildren);
        Task<IEnumerable<HotelConfiguration>> GetByIsAllowPets(bool isAllowPets);
        Task Create(HotelConfiguration hotelConfiguration);
        void Update(HotelConfiguration hotelConfiguration);
        Task Delete(int id);
    }
}
