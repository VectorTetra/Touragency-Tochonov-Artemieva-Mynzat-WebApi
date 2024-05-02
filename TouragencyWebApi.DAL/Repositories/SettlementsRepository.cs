using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;


namespace TouragencyWebApi.DAL.Repositories
{
    public class SettlementsRepository : ISettlementsRepository
    {
        private readonly TouragencyContext _context;
        public SettlementsRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Settlement>> GetAll()
        {
            return await _context.Settlements.ToListAsync();
        }
        public async Task<IEnumerable<Settlement>> Get200Last()
        {
            return await _context.Settlements.OrderByDescending(p => p.Id).Take(200).ToListAsync();
        }
        public async Task<Settlement?> GetById(int id)
        {
            return await _context.Settlements.FindAsync(id);
        }
        public async Task<IEnumerable<Settlement>> GetByName(string name)
        {
            return await _context.Settlements
                .Include(p => p.Country)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Settlement>> GetByCountryName(string countryName)
        {
            return await _context.Settlements
                .Where(p => p.Country.Name.Contains(countryName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Settlement>> GetByCountryId(int countryId) 
        {
            return await _context.Settlements.Where(p => p.Country.Id == countryId).ToListAsync();
        }
        
        public async Task<IEnumerable<Settlement>> GetByTourNameId(int tourNameId)
        {
            return await _context.Settlements
                .Where(p => p.TourNames.Any(t => t.Id == tourNameId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Settlement>> GetByTourName(string tourName)
        {
            return await _context.Settlements
                .Where(p => p.TourNames.Any(t => t.Name.Contains(tourName)))
                .ToListAsync();
        }
        public async Task<Settlement?> GetByHotelId(int hotelId) 
        {
            return await _context.Settlements.FirstOrDefaultAsync(p => p.Hotels.Any(h => h.Id == hotelId));
        }

        public async Task<IEnumerable<Settlement>> GetByCompositeSearch(string? name, string? countryName, int? countryId, int? tourNameId, string? tourName)
        {
            var settlementsCollections = new List<IEnumerable<Settlement>>();

            if (name != null)
            {
                var settlementsByName = await GetByName(name);
                settlementsCollections.Add(settlementsByName);
            }

            if (countryName != null)
            {
                var settlementsByCountryName = await GetByCountryName(countryName);
                settlementsCollections.Add(settlementsByCountryName);
            }

            if (countryId != null)
            {
                var settlementsByCountryId = await GetByCountryId((int)countryId);
                settlementsCollections.Add(settlementsByCountryId);
            }
            if (tourNameId != null)
            {
                var settlementsByTourNameId = await GetByTourNameId((int)tourNameId);
                settlementsCollections.Add(settlementsByTourNameId);
            }

            if (tourName != null)
            {
                var settlementsByTourName = await GetByTourName(tourName);
                settlementsCollections.Add(settlementsByTourName);
            }


            if (!settlementsCollections.Any())
            {
                return new List<Settlement>();
            }

            return settlementsCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task Create(Settlement settlement)
        {
            await _context.Settlements.AddAsync(settlement);
        }
        public void Update(Settlement settlement)
        {
            _context.Entry(settlement).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Settlement? settlement = await _context.Settlements.FindAsync(id);
            if (settlement != null)
                _context.Settlements.Remove(settlement);
        }
    }
}
