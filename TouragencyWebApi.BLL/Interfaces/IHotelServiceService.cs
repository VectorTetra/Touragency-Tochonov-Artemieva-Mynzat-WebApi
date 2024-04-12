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
        Task<HotelServiceDTO?> GetById(int id);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelServiceDTO>> GetByHotelServiceTypeId(int hotelServiceTypeId);
        Task<IEnumerable<HotelServiceDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<HotelServiceDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task Create(HotelServiceDTO hotelServiceDTO);
        Task Update(HotelServiceDTO hotelServiceDTO);
        Task Delete(int id);
    }
}
