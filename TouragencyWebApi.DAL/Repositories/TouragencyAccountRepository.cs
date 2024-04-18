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
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeName(string employeeName)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyEmployee.Person.Lastname.Contains(employeeName))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployeeAccount>> GetByRole(string role)
        {
            return await _context.TouragencyEmployeeAccounts
                .Where(p => p.TouragencyAccountRole.Name.Contains(role))
                .ToListAsync();
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
