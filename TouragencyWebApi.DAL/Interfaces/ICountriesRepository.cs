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
        Task<Country?> GetById(long id);
        Task<Country?> GetByName(string countryName);
        Task Create(Country country);
        void Update(Country country);
        Task Delete(long id);
    }
}
