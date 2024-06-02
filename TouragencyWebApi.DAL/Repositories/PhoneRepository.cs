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
        public async Task<IEnumerable<Phone>> Get200Last()
        {
            return await _context.Phones
                .OrderByDescending(p => p.Id)
                .Take(200)
                .ToListAsync();
        }
        public async Task<Phone?> GetById(long id)
        {
            return await _context.Phones.FindAsync(id);
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
                .Where(p => p.Persons.Any(p => p.Client.TouristNickname.Contains(touristNickname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByContactTypeId(int contactTypeId) 
        {
            return await _context.Phones
                .Where(p => p.ContactTypeId == contactTypeId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByPhoneNumber(string phoneNumberSubstring) 
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.PhoneNumber.Contains(phoneNumberSubstring))
                .ToListAsync();
        }
        public async Task Create(Phone phone) 
        {
            await _context.Phones.AddAsync(phone);
        }
        public void Update(Phone phone) 
        {
            _context.Entry(phone).State = EntityState.Modified;
        }
        public async Task Delete(long id) 
        {
            Phone? phone = await _context.Phones.FindAsync(id);
            if (phone != null)
                _context.Phones.Remove(phone);
        }

        public async Task<IEnumerable<Phone>> GetByFirstname(string firstname)
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Firstname.Contains(firstname)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Phone>> GetByLastname(string lastname)
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Lastname.Contains(lastname)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Phone>> GetByMiddlename(string middlename)
        {
            return await _context.Phones
                .Include(p => p.Persons)
                .Where(p => p.Persons.Any(p => p.Middlename.Contains(middlename)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Phone>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
                       string? touristNickname, int? contactTypeId, string? phoneNumberSubstring, string? firstname,
                                  string? lastname, string? middlename)
        {
            var phoneCollections = new List<IEnumerable<Phone>>();

            if (clientId != null)
            {
                phoneCollections.Add(await GetByClientId(clientId.Value));
            }
            if (personId != null)
            {
                phoneCollections.Add(await GetByPersonId(personId.Value));
            }
            if (touragencyEmployeeId != null)
            {
                phoneCollections.Add(await GetByTouragencyEmployeeId(touragencyEmployeeId.Value));
            }
            if (touristNickname != null)
            {
                phoneCollections.Add(await GetByTouristNickname(touristNickname));
            }
            if (contactTypeId != null)
            {
                phoneCollections.Add(await GetByContactTypeId(contactTypeId.Value));
            }
            if (phoneNumberSubstring != null)
            {
                phoneCollections.Add(await GetByPhoneNumber(phoneNumberSubstring));
            }
            if (firstname != null)
            {
                phoneCollections.Add(await GetByFirstname(firstname));
            }
            if (lastname != null)
            {
                phoneCollections.Add(await GetByLastname(lastname));
            }
            if (middlename != null)
            {
                phoneCollections.Add(await GetByMiddlename(middlename));
            }
            if(!phoneCollections.Any())
            {
                return new List<Phone>();
            }
            return phoneCollections.Aggregate((a, b) => a.Intersect(b));
        }
    }
}

