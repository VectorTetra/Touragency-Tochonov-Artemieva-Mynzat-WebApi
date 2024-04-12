using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelServiceTypeService
    {
        Task<IEnumerable<HotelServiceTypeDTO>> GetAll();
        Task<HotelServiceTypeDTO?> GetById(int id);
        Task<IEnumerable<HotelServiceTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelServiceTypeDTO>> GetByHotelServiceId(int hotelServiceId);
        Task Create(HotelServiceTypeDTO hotelServiceTypeDTO);
        Task Update(HotelServiceTypeDTO hotelServiceTypeDTO);
        Task Delete(int id);
    }
}
