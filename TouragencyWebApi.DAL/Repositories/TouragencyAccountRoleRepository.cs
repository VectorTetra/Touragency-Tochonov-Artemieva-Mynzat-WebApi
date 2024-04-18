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
    public class TouragencyAccountRoleRepository : ITouragencyAccountRoleRepository
    {
        private readonly TouragencyContext _context;
        public TouragencyAccountRoleRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetAll()
        {
            return await _context.TouragencyAccountRoles.ToListAsync();
        }
        public async Task<TouragencyAccountRole?> GetById(int id)
        {
            return await _context.TouragencyAccountRoles.FindAsync(id);
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByName(string name)
        {
            return await _context.TouragencyAccountRoles
                .Include(p => p.TouragencyEmployeeAccounts)
                .Include(p => p.Clients)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeName(string employeeName)
        {
            return await _context.TouragencyAccountRoles
                .Include(p => p.TouragencyEmployeeAccounts)
                .Include(p => p.Clients)
                .Where(p => p.TouragencyEmployeeAccounts.Any(p => p.TouragencyEmployee.Person.Lastname.Contains(employeeName)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByClientName(string clientName)
        {
            return await _context.TouragencyAccountRoles
                .Include(p => p.TouragencyEmployeeAccounts)
                .Include(p => p.Clients)
                .Where(p => p.Clients.Any(p => p.Person.Lastname.Contains(clientName)))
                .ToListAsync();
        }
        public async Task Create(TouragencyAccountRole entity)
        {
            await _context.TouragencyAccountRoles.AddAsync(entity);
        }
        public void Update(TouragencyAccountRole entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            TouragencyAccountRole? entity = await _context.TouragencyAccountRoles.FindAsync(id);
            if (entity != null)
                _context.TouragencyAccountRoles.Remove(entity);
        }
    }
}
