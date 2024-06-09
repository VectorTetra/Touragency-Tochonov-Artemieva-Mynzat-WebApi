using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelServiceRepository
    {
        Task<IEnumerable<HotelService>> GetAll();
        Task<IEnumerable<HotelService>> Get200Last();
        Task<HotelService?> GetById(int id);
        Task<IEnumerable<HotelService>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelService>> GetByHotelServiceTypeId(int hotelServiceTypeId);
        Task<IEnumerable<HotelService>> GetByHotelServiceTypeNameSubstring(string hotelServiceTypeNameSubstring);
        Task<IEnumerable<HotelService>> GetByHotelNameSubstring(string hotelNameSubstring);
        Task<IEnumerable<HotelService>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<HotelService>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelService>> GetByCompositeSearch(int? hotelId, int? hotelServiceTypeId, string? hotelServiceTypeNameSubstring,
            string? hotelNameSubstring,string? nameSubstring, string? descriptionSubstring);
        Task Create(HotelService hotelService);
        void Update(HotelService hotelService);
        Task Delete(int id);
    }
}
