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
        Task<Country?> GetById(int id);
        Task<IEnumerable<Country>> GetByName(string countryName);
        Task Create(Country country);
        void Update(Country country);
        Task Delete(int id);
    }
}
