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
        Task<CountryDTO?> GetById(int id);
        Task<IEnumerable<CountryDTO>> GetByName(string countryName);
        Task Add(CountryDTO countryDTO);
        Task Update(CountryDTO countryDTO);
        Task Delete(int id);
    }
}
