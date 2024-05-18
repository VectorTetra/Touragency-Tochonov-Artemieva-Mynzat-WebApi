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
        Task<IEnumerable<Settlement>> Get200Last();
        Task<Settlement?> GetById(int id);
        Task<IEnumerable<Settlement>> GetByName(string name);
        Task<IEnumerable<Settlement>> GetByCountryName(string countryName);
        Task<IEnumerable<Settlement>> GetByCountryId(int countryId);
        Task<IEnumerable<Settlement>> GetByCountryIds(int[] countryId);
        Task<IEnumerable<Settlement>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Settlement>> GetByTourName(string tourName);
        Task<IEnumerable<Settlement>> GetByCompositeSearch(string? name, string? countryName, int? countryId, int? tourNameId, string? tourName, int[]? countryIds);
        Task<Settlement?> GetByHotelId(int hotelId);
        Task Create(Settlement settlement);
        void Update(Settlement settlement);
        Task Delete(int id);
    }
}
