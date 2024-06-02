using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace TouragencyWebApi.DAL.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly TouragencyContext _context;
        public CountriesRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.Countries.OrderBy(p=>p.Name).ToListAsync();
        }
        public async Task<IEnumerable<Country>> Get200Last()
        {
            return await _context.Countries.OrderByDescending(p => p.Id).Take(200).ToListAsync();
        }
        public async Task<Country?> GetById(int id)
        {
            return await _context.Countries.FindAsync(id);
        }
        public async Task<IEnumerable<Country>> GetByName(string countryName)
        {
            return await _context.Countries
                .Include(p => p.Settlements)
                .Where(p => p.Name.Contains(countryName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetByContinentName(string continentName)
        {
            return await _context.Countries
                .Where(p => p.Continent.Name.Contains(continentName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetByContinentId(int continentId)
        {
            return await _context.Countries
                .Where(p => p.Continent.Id == continentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetByTourNameId(int tourNameId)
        {
            return await _context.Countries
                .Where(p => p.TourNames.Any(t => t.Id == tourNameId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetByTourName(string tourName)
        {
            return await _context.Countries
                .Where(p => p.TourNames.Any(t => t.Name.Contains(tourName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetByCompositeSearch(string? name, string? continentName, int? continentId, int? tourNameId, string? tourName)
        {
            var collections = new List<IEnumerable<Country>>();
            if (name != null)
            {
                collections.Add(await GetByName(name));
            }
            if (continentName != null)
            {
                collections.Add(await GetByContinentName(continentName));
            }
            if (continentId != null)
            {
                collections.Add(await GetByContinentId(continentId.Value));
            }
            if (tourNameId != null)
            {
                collections.Add(await GetByTourNameId(tourNameId.Value));
            }
            if (tourName != null)
            {
                collections.Add(await GetByTourName(tourName));
            }
            if(!collections.Any())
            {
                return new List<Country>();
            }
            return collections.Aggregate((a, b) => a.Intersect(b));
        }
        public async Task Create(Country country)
        {
            await _context.Countries.AddAsync(country);
        }
        public void Update(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Country? country = await _context.Countries.FindAsync(id);
            if (country != null)
                _context.Countries.Remove(country);
        }
    }
}
