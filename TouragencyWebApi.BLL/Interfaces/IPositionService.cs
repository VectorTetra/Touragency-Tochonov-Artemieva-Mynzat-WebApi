using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IPositionService
    {
        Task<PositionDTO> Create(PositionDTO position);
        Task<PositionDTO> Update(PositionDTO position);
        Task<PositionDTO> Delete(int id);
        Task<IEnumerable<PositionDTO>> GetAll();
        Task<IEnumerable<PositionDTO>> Get200Last();
        Task<IEnumerable<PositionDTO>> GetByDescriptionSubstring(string positionDescriptionSubstring);
        Task<IEnumerable<PositionDTO>> GetByNameSubstring(string positionNameSubstring);
        Task<PositionDTO> GetByPersonId(int id);
        Task<PositionDTO> GetByTouragencyEmployeeId(int id);
        Task<PositionDTO> GetById(int id);
        Task<IEnumerable<PositionDTO>> GetByPersonFirstnameSubstring(string personFirstnameSubstring);
        Task<IEnumerable<PositionDTO>> GetByPersonLastnameSubstring(string personLastnameSubstring);
        Task<IEnumerable<PositionDTO>> GetByPersonMiddlenameSubstring(string personMiddlenameSubstring);
        Task<IEnumerable<PositionDTO>> GetByCompositeSearch(string? positionNameSubstring, string? positionDescriptionSubstring,
            string? personFirstnameSubstring, string? personLastnameSubstring, string? personMiddlenameSubstring);
    }
}
