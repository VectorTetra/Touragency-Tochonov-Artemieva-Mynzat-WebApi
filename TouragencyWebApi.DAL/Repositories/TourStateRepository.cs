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
    public class TourStateRepository : ITourStateRepository
    {
        private readonly TouragencyContext _context;
        public TourStateRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TourState>> GetAll()
        {
            return await _context.TourStates.ToListAsync();
        }
        public async Task<TourState?> GetById(int id)
        {
            return await _context.TourStates.FindAsync(id);
        }
        public async Task<IEnumerable<TourState>> GetByStatus(string status)
        {
            return await _context.TourStates
                .Include(p => p.Tours)
                .Where(p => p.Status == status)
                .ToListAsync();
        }
        public async Task Create(TourState tourstate)
        {
            await _context.TourStates.AddAsync(tourstate);
        }
        public void Update(TourState tourstate)
        {
            _context.Entry(tourstate).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            TourState? tourstate = await _context.TourStates.FindAsync(id);
            if (tourstate != null)
                _context.TourStates.Remove(tourstate);
        }
    }
}
