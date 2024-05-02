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
        Task<IEnumerable<TransportTypeDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TransportTypeDTO>> GetByTourName(string tourName);
        Task<IEnumerable<TransportTypeDTO>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
           int? tourNameId, string? tourname);
        Task<TransportTypeDTO> Create(TransportTypeDTO transportType);
        Task<TransportTypeDTO> Update(TransportTypeDTO transportType);
        Task<TransportTypeDTO> Delete(int id);
    }
}
