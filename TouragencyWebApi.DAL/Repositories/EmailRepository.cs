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
    public class EmailRepository : IEmailRepository
    {
        private readonly TouragencyContext _context;
        public EmailRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Email>> GetAll()
        {
            return await _context.Emails.ToListAsync();
        }
        public async Task<Email?> GetById(long id)
        {
            return await _context.Emails.FindAsync(id);
        }
        public async Task<IEnumerable<Email>> GetByClientId(int clientId)
        {
            return await _context.Emails
                 .Include(p => p.Persons)
                 .Where(p => p.Persons.Any(p => p.Client.Id == clientId))
                 .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByPersonId(int personId)
        {
            return await _context.Emails
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Id == personId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByTouragencyEmployeeId(int touragencyEmployeeId)
        {
            return await _context.Emails
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.TouragencyEmployee.Id == touragencyEmployeeId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByTouristNickname(string touristNickname)
        {
            return await _context.Emails
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Client.TouristNickname == touristNickname))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByContactTypeId(int contactTypeId)
        {
            return await _context.Emails
                .Include(p => p.Persons)
                .Where(p => p.ContactTypeId == contactTypeId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByEmailAddress(string emailAddressSubstring)
        {
            return await _context.Emails
                .Include(p => p.Persons)
                .Where(p => p.EmailAddress.Contains(emailAddressSubstring))
                .ToListAsync();
        }
        public async Task Create(Email Email)
        {
            await _context.Emails.AddAsync(Email);
        }
        public void Update(Email Email)
        {
            _context.Entry(Email).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            Email? Email = await _context.Emails.FindAsync(id);
            if (Email != null)
                _context.Emails.Remove(Email);
        }
    }
}

