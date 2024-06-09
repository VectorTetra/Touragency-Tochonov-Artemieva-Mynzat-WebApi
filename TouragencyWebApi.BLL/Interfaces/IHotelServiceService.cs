using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelServiceService
    {
        Task<IEnumerable<HotelServiceDTO>> GetAll();
        Task<IEnumerable<HotelServiceDTO>> Get200Last();
        Task<HotelServiceDTO?> GetById(int id);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelServiceTypeId(int hotelServiceTypeId);
        Task<IEnumerable<HotelServiceDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<HotelServiceDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelServiceTypeNameSubstring(string hotelServiceTypeNameSubstring);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelNameSubstring(string hotelNameSubstring);
        Task<IEnumerable<HotelServiceDTO>> GetByCompositeSearch(int? hotelId, int? hotelServiceTypeId, string? hotelServiceTypeNameSubstring,
            string? hotelNameSubstring, string? nameSubstring, string? descriptionSubstring);
        Task<HotelServiceDTO> Create(HotelServiceDTO hotelServiceDTO);
        Task<HotelServiceDTO> Update(HotelServiceDTO hotelServiceDTO);
        Task<HotelServiceDTO> Delete(int id);
    }
}
