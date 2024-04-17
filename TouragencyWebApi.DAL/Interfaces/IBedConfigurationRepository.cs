using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IBedConfigurationRepository
    {
        Task<BedConfiguration?> GetById(int id);
        Task<IEnumerable<BedConfiguration>> GetAll();
        Task<IEnumerable<BedConfiguration>> GetByHotelId(int hotelId);
        Task<IEnumerable<BedConfiguration>> GetByBookingDataId(long bookingDataId);
        Task<IEnumerable<BedConfiguration>> GetByCapacity(short capacity);
        Task<IEnumerable<BedConfiguration>> GetByLabelSubstring(string labelSubstring);
        Task<IEnumerable<BedConfiguration>> GetByDescriptionSubstring(string descriptionSubstring);
        Task Create(BedConfiguration bedConfiguration);
        void Update(BedConfiguration bedConfiguration);
        Task Delete(int id);
    }
}
