using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.DAL.EF;

namespace TouragencyWebApi.DAL.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly TouragencyContext _context;
        public PositionRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task Create(Position position)
        {
            await _context.Positions.AddAsync(position);
        }
        public void Update(Position position)
        {
            _context.Entry(position).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Position? position = await _context.Positions.FindAsync(id);
            if (position != null)
                _context.Positions.Remove(position);
        }
        public async Task<IEnumerable<Position>> GetAll()
        {
            return await _context.Positions.ToListAsync();
        }
        public async Task<IEnumerable<Position>> GetByDescriptionSubstring(string positionDescriptionSubstring)
        {
            return await _context.Positions
                .Where(p => p.Description.Contains(positionDescriptionSubstring))
                .ToListAsync();
        }
        public async Task<Position?> GetByPersonId(int id)
        {
            return await _context.Positions
                .FirstOrDefaultAsync(p => p.TouragencyEmployees.Any(t => t.PersonId == id) );
        }
        public async Task<Position?> GetByTouragencyEmployeeId(int id)
        {
            return await _context.Positions
                .FirstOrDefaultAsync(p => p.TouragencyEmployees.Any(t => t.Id == id));
        }
        public async Task<Position?> GetById(int id)
        {
            return await _context.Positions.FindAsync(id);
        }
    }
}
