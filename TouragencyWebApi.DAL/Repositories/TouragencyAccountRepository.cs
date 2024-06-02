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
    public class TouragencyAccountRepository : ITouragencyAccountRepository
    {
        private readonly TouragencyContext _context;
        public TouragencyAccountRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetAll()
        {
            return await _context.TouragencyEmployeeAccounts.ToListAsync();
        }

        public async Task<IEnumerable<TouragencyEmployeeAccount>> Get200Last()
        {
            return await _context.TouragencyEmployeeAccounts
                .OrderByDescending(p => p.Id)
                .Take(200)
                .ToListAsync();
        }
        public async Task<TouragencyEmployeeAccount?> GetById(int id)
        {
            return await _context.TouragencyEmployeeAccounts.FindAsync(id);
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByLogin(string login)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.Login.Contains(login))
                .ToListAsync();
        }

        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByRoleName(string roleName)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyAccountRole.Name.Contains(roleName))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByRoleDescription(string roleDescription)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyAccountRole.Description.Contains(roleDescription))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeFirstname(string employeeFirstname)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyEmployee.Person.Firstname.Contains(employeeFirstname))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeLastname(string employeeLastname)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyEmployee.Person.Lastname.Contains(employeeLastname))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeMiddlename(string employeeMiddlename)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyEmployee.Person.Middlename.Contains(employeeMiddlename))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByCompositeSearch(string? login, string? roleName,
                       string? roleDescription, string? employeeFirstname, string? employeeLastname, string? employeeMiddlename)
        {
            var accountsCollection = new List<IEnumerable<TouragencyEmployeeAccount>>();
            if (login != null)
            { 
                accountsCollection.Add(await GetByLogin(login));
            }
            if (roleName != null)
            {
                accountsCollection.Add(await GetByRoleName(roleName));
            }
            if (roleDescription != null)
            {
                accountsCollection.Add(await GetByRoleDescription(roleDescription));
            }
            if (employeeFirstname != null)
            {
                accountsCollection.Add(await GetByEmployeeFirstname(employeeFirstname));
            }
            if (employeeLastname != null)
            {
                accountsCollection.Add(await GetByEmployeeLastname(employeeLastname));
            }
            if (employeeMiddlename != null)
            {
                accountsCollection.Add(await GetByEmployeeMiddlename(employeeMiddlename));
            }
            if(!accountsCollection.Any())
            {
                return new List<TouragencyEmployeeAccount>();
            }
            return accountsCollection.Aggregate((acc, next) => acc.Intersect(next));
        }


        public async Task Create(TouragencyEmployeeAccount account)
        {
            await _context.TouragencyEmployeeAccounts.AddAsync(account);
        }
        public void Update(TouragencyEmployeeAccount account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            TouragencyEmployeeAccount? account = await _context.TouragencyEmployeeAccounts.FindAsync(id);
            if (account != null)
                _context.TouragencyEmployeeAccounts.Remove(account);
        }
    }
}
