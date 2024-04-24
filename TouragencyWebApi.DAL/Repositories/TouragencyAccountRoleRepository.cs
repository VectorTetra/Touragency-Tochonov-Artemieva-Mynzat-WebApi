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
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TouragencyAccountRole>> GetByDescription(string description)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.Description.Contains(description))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeFirstname(string employeeFirstname)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.TouragencyEmployeeAccounts.Any(p => p.TouragencyEmployee.Person.Firstname.Contains(employeeFirstname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeLastname(string employeeLastname)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.TouragencyEmployeeAccounts.Any(p => p.TouragencyEmployee.Person.Lastname.Contains(employeeLastname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeMiddlename(string employeeMiddlename)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.TouragencyEmployeeAccounts.Any(p => p.TouragencyEmployee.Person.Middlename.Contains(employeeMiddlename)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByClientFirstname(string clientFirstname)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.Clients.Any(p => p.Person.Firstname.Contains(clientFirstname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByClientLastname(string clientLastname)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.Clients.Any(p => p.Person.Lastname.Contains(clientLastname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyAccountRole>> GetByClientMiddlename(string clientMiddlename)
        {
            return await _context.TouragencyAccountRoles
                .Where(p => p.Clients.Any(p => p.Person.Middlename.Contains(clientMiddlename)))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TouragencyAccountRole>> GetByCompositeSearch(string? name, string? description, string? employeeFirstname,
                       string? employeeLastname, string? employeeMiddlename, string? clientFirstname, string? clientLastname, string? clientMiddlename)
        {
            var accCollections = new List<IEnumerable<TouragencyAccountRole>>();
            if (name != null)
            {
                accCollections.Add(await GetByName(name));
            }
            if (description != null)
            {
                accCollections.Add(await GetByDescription(description));
            }
            if (employeeFirstname != null)
            {
                accCollections.Add(await GetByEmployeeFirstname(employeeFirstname));
            }
            if (employeeLastname != null)
            {
                accCollections.Add(await GetByEmployeeLastname(employeeLastname));
            }
            if (employeeMiddlename != null)
            {
                accCollections.Add(await GetByEmployeeMiddlename(employeeMiddlename));
            }
            if (clientFirstname != null)
            {
                accCollections.Add(await GetByClientFirstname(clientFirstname));
            }
            if (clientLastname != null)
            {
                accCollections.Add(await GetByClientLastname(clientLastname));
            }
            if (clientMiddlename != null)
            {
                accCollections.Add(await GetByClientMiddlename(clientMiddlename));
            }
            if(!accCollections.Any())
            {
                return new List<TouragencyAccountRole>();
            }
            return accCollections.Aggregate((acc, next) => acc.Intersect(next));
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
