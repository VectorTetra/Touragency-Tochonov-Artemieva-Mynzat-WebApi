using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITransportTypeService
    {
        Task<IEnumerable<TransportTypeDTO>> GetAll();
        Task<TransportTypeDTO?> GetById(int id);
        Task<IEnumerable<TransportTypeDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<TransportTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<TransportTypeDTO>> GetByTourId(long tourId);
        Task Create(TransportTypeDTO transportType);
        Task Update(TransportTypeDTO transportType);
        Task Delete(int id);
    }
}
