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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly TouragencyContext _context;
        public PhoneRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Phone>> GetAll()
        {
            return await _context.Phones.ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByClientId(int clientId)
        {
           return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Client.Id == clientId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByPersonId(int personId) 
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Id == personId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByTouragencyEmployeeId(int touragencyEmployeeId) 
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.TouragencyEmployee.Id == touragencyEmployeeId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByTouristNickname(string touristNickname)
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Client.TouristNickname == touristNickname))
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByContactTypeId(int contactTypeId) 
        {
            return await _context.Phones
                .Where(p => p.ContactTypeId == contactTypeId)
                .ToListAsync();
        }
        public async Task<Phone?> GetByPhoneNumber(string phoneNumber) 
        {
            return await _context.Phones
                .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }
        public async Task Create(Phone phone) 
        {
            await _context.Phones.AddAsync(phone);
        }
        public void Update(Phone phone) 
        {
            _context.Entry(phone).State = EntityState.Modified;
        }
        public async Task Delete(int id) 
        {
            Phone? phone = await _context.Phones.FindAsync(id);
            if (phone != null)
                _context.Phones.Remove(phone);
        }
    }
}

