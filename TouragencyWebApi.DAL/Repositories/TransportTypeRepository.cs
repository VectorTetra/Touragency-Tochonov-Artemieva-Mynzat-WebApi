using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TransportTypeRepository : ITransportTypeRepository
    {
        private readonly TouragencyContext _context;

        public TransportTypeRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransportType>> GetAll()
        {
            return await _context.TransportTypes.ToListAsync();
        }

        public async Task<TransportType?> GetById(int id)
        {
            return await _context.TransportTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TransportType>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.TransportTypes.Where(t => t.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<TransportType>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.TransportTypes.Where(t => t.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<TransportType>> GetByTourNameId(int tourNameId)
        {
            return await _context.TransportTypes.Where(t => t.TourNames.Any(t => t.Id == tourNameId)).ToListAsync();
        }

        public async Task<IEnumerable<TransportType>> GetByTourName(string tourname)
        {
            return await _context.TransportTypes.Where(t => t.TourNames.Any(t => t.Name.Contains(tourname))).ToListAsync();
        }

        public async Task<IEnumerable<TransportType>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
           int? tourNameId, string? tourname)
        {
            var typeCollections = new List<IEnumerable<TransportType>>();
            
            if (nameSubstring != null)
            {
                typeCollections.Add(await GetByNameSubstring(nameSubstring));
            }
            if (descriptionSubstring != null)
            {
                typeCollections.Add(await GetByDescriptionSubstring(descriptionSubstring));
            }
            if (tourNameId != null)
            {
                typeCollections.Add(await GetByTourNameId(tourNameId.Value));
            }
            if (tourname != null)
            {
                typeCollections.Add(await GetByTourName(tourname));
            }
            if(!typeCollections.Any())
            {
                return new List<TransportType>();
            }
            return typeCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }

        public async Task Create(TransportType transportType)
        {
            await _context.TransportTypes.AddAsync(transportType);
        }


        public void Update(TransportType transportType)
        {
            _context.Entry(transportType).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var transportType = await _context.TransportTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (transportType != null)
            {
                _context.TransportTypes.Remove(transportType);
            }
        }
    }
}
