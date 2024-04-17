using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IBedConfigurationService
    {
        Task<BedConfigurationDTO?> GetById(int id);
        Task<IEnumerable<BedConfigurationDTO>> GetAll();
        Task<IEnumerable<BedConfigurationDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<BedConfigurationDTO>> GetByBookingDataId(long bookingDataId);
        Task<IEnumerable<BedConfigurationDTO>> GetByCapacity(short capacity);
        Task<IEnumerable<BedConfigurationDTO>> GetByLabelSubstring(string labelSubstring);
        Task<IEnumerable<BedConfigurationDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task Create(BedConfigurationDTO BedConfigurationDTO);
        Task Update(BedConfigurationDTO BedConfigurationDTO);
        Task Delete(int id);
    }
}
