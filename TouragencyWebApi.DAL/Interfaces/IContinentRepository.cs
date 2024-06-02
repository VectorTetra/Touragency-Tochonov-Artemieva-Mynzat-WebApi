using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IContinentRepository
    {
        Task<IEnumerable<Continent>> GetAll();
        Task<Continent> GetById(int id);
        Task<IEnumerable<Continent>> GetByName(string name);
        Task<IEnumerable<Continent>> GetByCountryName(string countryName);
        Task<IEnumerable<Continent>> GetByCountryId(int countryId);
        Task<IEnumerable<Continent>> GetByCompositeSearch(string? name, string? countryName, int? countryId);
        Task Create(Continent continent);
        void Update(Continent continent);
        Task Delete(int id);
    }
}
