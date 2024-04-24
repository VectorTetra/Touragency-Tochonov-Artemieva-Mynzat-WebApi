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

        public async Task<IEnumerable<Position>> Get200Last()
        {
            return await _context.Positions.OrderByDescending(p => p.Id).Take(200).ToListAsync();
        }
        public async Task<IEnumerable<Position>> GetByDescriptionSubstring(string positionDescriptionSubstring)
        {
            return await _context.Positions
                .Where(p => p.Description.Contains(positionDescriptionSubstring))
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetByNameSubstring(string positionNameSubstring)
        {
            return await _context.Positions
                .Where(p => p.Name.Contains(positionNameSubstring))
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

        public async Task<IEnumerable<Position>> GetByCompositeSearch(string? positionNameSubstring, string? positionDescriptionSubstring,
                       string? personFirstnameSubstring, string? personLastnameSubstring, string? personMiddlenameSubstring)
        {
            var positions = new List<IEnumerable<Position>>();
            if (positionNameSubstring != null)
            {
                positions.Add(await GetByNameSubstring(positionNameSubstring));
            }
            if (positionDescriptionSubstring != null)
            {
                positions.Add(await GetByDescriptionSubstring(positionDescriptionSubstring));
            }
            if (personFirstnameSubstring != null)
            {
                positions.Add(await GetByPersonFirstnameSubstring(personFirstnameSubstring));
            }
            if (personLastnameSubstring != null)
            {
                positions.Add(await GetByPersonLastnameSubstring(personLastnameSubstring));
            }
            if (personMiddlenameSubstring != null)
            {
                positions.Add(await GetByPersonMiddlenameSubstring(personMiddlenameSubstring));
            }
            if(!positions.Any())
            {
                return new List<Position>();
            }
            return positions.Aggregate((a, b) => a.Intersect(b));
        }

        public async Task<IEnumerable<Position>> GetByPersonFirstnameSubstring(string personFirstnameSubstring)
        {
            return await _context.Positions
                .Where(p => p.TouragencyEmployees.Any(t => t.Person.Firstname.Contains(personFirstnameSubstring)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetByPersonLastnameSubstring(string personLastnameSubstring)
        {
            return await _context.Positions
                .Where(p => p.TouragencyEmployees.Any(t => t.Person.Lastname.Contains(personLastnameSubstring)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetByPersonMiddlenameSubstring(string personMiddlenameSubstring)
        {
            return await _context.Positions
                .Where(p => p.TouragencyEmployees.Any(t => t.Person.Middlename.Contains(personMiddlenameSubstring)))
                .ToListAsync();
        }
    }
}
