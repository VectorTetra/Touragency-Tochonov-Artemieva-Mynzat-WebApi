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
