using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class ContinentRepository: IContinentRepository
    {
        private readonly TouragencyContext _context;
        public ContinentRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Continent>> GetAll()
        {
            return await _context.Continents.ToListAsync();
        }

        public async Task<Continent> GetById(int id)
        {
            return await _context.Continents.FindAsync(id);
        }

        public async Task<IEnumerable<Continent>> GetByName(string name)
        {
            return await _context.Continents
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Continent>> GetByCountryName(string countryName)
        {
            return await _context.Continents
                .Where(p => p.Countries.Any(c => c.Name.Contains(countryName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Continent>> GetByCountryId(int countryId)
        {
            return await _context.Continents
                .Where(p => p.Countries.Any(c => c.Id == countryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Continent>> GetByCompositeSearch(string? name, string? countryName, int? countryId)
        {
            var continentCollections = new List<IEnumerable<Continent>>();
            
            if (name != null)
            {
                continentCollections.Add(await GetByName(name));
            }
            if (countryName != null)
            {
                continentCollections.Add(await GetByCountryName(countryName));
            }
            if (countryId != null)
            {
                continentCollections.Add(await GetByCountryId(countryId.Value));
            }
            if(!continentCollections.Any())
            {
                return new List<Continent>();
            }
            return continentCollections.Aggregate((a, b) => a.Intersect(b));
        }

        public async Task Create(Continent continent)
        {
            await _context.Continents.AddAsync(continent);
        }

        public void Update(Continent continent)
        {
            _context.Entry(continent).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent != null)
            {
                _context.Continents.Remove(continent);
            }
        }
    }
}
