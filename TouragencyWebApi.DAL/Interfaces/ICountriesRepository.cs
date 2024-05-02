using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ICountriesRepository
    {
        Task<IEnumerable<Country>> GetAll();
        Task<IEnumerable<Country>> Get200Last();
        Task<Country?> GetById(int id);
        Task<IEnumerable<Country>> GetByName(string name);
        Task<IEnumerable<Country>> GetByContinentName(string continentName);
        Task<IEnumerable<Country>> GetByContinentId(int continentId);
        Task<IEnumerable<Country>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Country>> GetByTourName(string tourName);
        Task<IEnumerable<Country>> GetByCompositeSearch(string? name, string? continentName, int? continentId, int? tourNameId, string? tourName);
        Task Create(Country country);
        void Update(Country country);
        Task Delete(int id);
    }
}
