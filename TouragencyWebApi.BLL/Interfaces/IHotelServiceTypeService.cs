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
        Task<IEnumerable<HotelServiceTypeDTO>> Get200Last();
        Task<HotelServiceTypeDTO?> GetById(int id);
        Task<IEnumerable<HotelServiceTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelServiceTypeDTO>> GetByHotelServiceId(int hotelServiceId);
        Task<HotelServiceTypeDTO> Create(HotelServiceTypeDTO hotelServiceTypeDTO);
        Task<HotelServiceTypeDTO> Update(HotelServiceTypeDTO hotelServiceTypeDTO);
        Task<HotelServiceTypeDTO> Delete(int id);
    }
}
