using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IPositionService
    {
        Task Create(PositionDTO position);
        Task Update(PositionDTO position);
        Task Delete(int id);
        Task<IEnumerable<PositionDTO>> GetAll();
        Task<IEnumerable<PositionDTO>> GetByDescriptionSubstring(string positionDescriptionSubstring);
        Task<PositionDTO> GetByPersonId(int id);
        Task<PositionDTO> GetByTouragencyEmployeeId(int id);
        Task<PositionDTO> GetById(int id);
    }
}
