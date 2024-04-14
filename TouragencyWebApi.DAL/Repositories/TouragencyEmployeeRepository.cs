using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TouragencyEmployeeRepository : ITouragencyEmployeeRepository
    {
        private readonly TouragencyContext _context;
        public TouragencyEmployeeRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TouragencyEmployee>> GetAll()
        {
            return await _context.TouragencyEmployees.ToListAsync();
        }
        public async Task<TouragencyEmployee?> GetById(int id)
        {
            return await _context.TouragencyEmployees.FindAsync(id);
        }
        public async Task<IEnumerable<TouragencyEmployee>> GetByName(string name)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Person.Lastname.Contains(name))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployee>> GetByPosition(string position)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Position.Name.Contains(position))
                .ToListAsync();
        }
        public async Task Create(TouragencyEmployee employee)
        {
            await _context.TouragencyEmployees.AddAsync(employee);
        }
        public void Update(TouragencyEmployee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            TouragencyEmployee? employee = await _context.TouragencyEmployees.FindAsync(id);
            if (employee != null)
                _context.TouragencyEmployees.Remove(employee);
        }
    }
}
