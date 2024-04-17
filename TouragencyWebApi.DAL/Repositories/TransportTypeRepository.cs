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

        public async Task<IEnumerable<TransportType>> GetByTourId(long tourId)
        {
            return await _context.TransportTypes.Where(t => t.Tours.Any(t => t.Id == tourId)).ToListAsync();
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
