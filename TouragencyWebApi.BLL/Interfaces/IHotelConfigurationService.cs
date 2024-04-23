using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelConfigurationService
    {
        Task<IEnumerable<HotelConfigurationDTO>> GetAll();
        Task<IEnumerable<HotelConfigurationDTO>> Get200Last();
        Task<HotelConfigurationDTO?> GetById(int id);
        Task<IEnumerable<HotelConfigurationDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<HotelConfigurationDTO>> GetByCompassSideSubstring(string compassSideSubstring);
        Task<IEnumerable<HotelConfigurationDTO>> GetByWindowViewSubstring(string WindowViewSubstring);
        Task<IEnumerable<HotelConfigurationDTO>> GetByIsAllowChildren(bool isAllowChildren);
        Task<IEnumerable<HotelConfigurationDTO>> GetByIsAllowPets(bool isAllowPets);
        Task<IEnumerable<HotelConfigurationDTO>> GetByCompositeSearch(int? hotelId, string? compassSideSubstring, string? WindowViewSubstring,
            bool? isAllowChildren, bool? isAllowPets);
        Task<HotelConfigurationDTO> Create(HotelConfigurationDTO hotelConfigurationDTO);
        Task<HotelConfigurationDTO> Update(HotelConfigurationDTO hotelConfigurationDTO);
        Task<HotelConfigurationDTO> Delete(int id);
    }
}
