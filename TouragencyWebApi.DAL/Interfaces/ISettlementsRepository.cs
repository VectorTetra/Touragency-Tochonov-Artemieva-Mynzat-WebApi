using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ISettlementsRepository
    {
        Task<IEnumerable<Settlement>> GetAll();
        Task<Settlement?> GetById(int id);
        Task<IEnumerable<Settlement>> GetByName(string name);
        Task<IEnumerable<Settlement>> GetByCountryName(string countryName);
        Task Create(Settlement settlement);
        void Update(Settlement settlement);
        Task Delete(int id);
    }
}
