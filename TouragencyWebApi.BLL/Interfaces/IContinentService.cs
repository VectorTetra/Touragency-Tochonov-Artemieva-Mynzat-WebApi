using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IContinentService
    {
        Task<IEnumerable<ContinentDTO>> GetAll();
        Task<ContinentDTO> GetById(int id);
        Task<IEnumerable<ContinentDTO>> GetByName(string name);
        Task<IEnumerable<ContinentDTO>> GetByCountryName(string countryName);
        Task<IEnumerable<ContinentDTO>> GetByCountryId(int countryId);
        Task<IEnumerable<ContinentDTO>> GetByCompositeSearch(string? name, string? countryName, int? countryId);
        Task<ContinentDTO> Create(ContinentDTO continent);
        Task<ContinentDTO> Update(ContinentDTO continent);
        Task<ContinentDTO> Delete(int id);
    }
}
