using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAll();
        Task<IEnumerable<CountryDTO>> Get200Last();
        Task<CountryDTO?> GetById(int id);
        Task<IEnumerable<CountryDTO>> GetByName(string countryName);
        Task<IEnumerable<CountryDTO>> GetByContinentName(string continentName);
        Task<IEnumerable<CountryDTO>> GetByContinentId(int continentId);
        Task<IEnumerable<CountryDTO>> GetByCompositeSearch(string? name, string? continentName, int? continentId);
        Task<CountryDTO> Add(CountryDTO countryDTO);
        Task<CountryDTO> Update(CountryDTO countryDTO);
        Task<CountryDTO> Delete(int id);
    }
}
