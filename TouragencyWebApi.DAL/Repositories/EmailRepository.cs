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

        public async Task<IEnumerable<Email>> Get200Last()
        {
            return await _context.Emails
                .OrderByDescending(p => p.Id)
                .Take(200)
                .ToListAsync();
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
                .Where(p => p.Persons.Any(p => p.Client.TouristNickname.Contains(touristNickname)))
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

        public async Task<IEnumerable<Email>> GetByFirstname(string firstname)
        {
            return await _context.Emails
                .Where(p => p.Persons.Any(p => p.Firstname.Contains(firstname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByLastname(string lastname)
        {
            return await _context.Emails
                .Where(p => p.Persons.Any(p => p.Lastname.Contains(lastname)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByMiddlename(string middlename)
        {
            return await _context.Emails
                .Where(p => p.Persons.Any(p => p.Middlename.Contains(middlename)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
                       string? touristNickname, int? contactTypeId, string? emailAddressSubstring, string? firstname,
                                  string? lastname, string? middlename)
        {
            var emailCollections = new List<IEnumerable<Email>>();
            if (clientId != null)
            {
                emailCollections.Add(await GetByClientId(clientId.Value));
            }
            if (personId != null)
            {
                emailCollections.Add(await GetByPersonId(personId.Value));
            }
            if (touragencyEmployeeId != null)
            {
                emailCollections.Add(await GetByTouragencyEmployeeId(touragencyEmployeeId.Value));
            }
            if (touristNickname != null)
            {
                emailCollections.Add(await GetByTouristNickname(touristNickname));
            }
            if (contactTypeId != null)
            {
                emailCollections.Add(await GetByContactTypeId(contactTypeId.Value));
            }
            if (emailAddressSubstring != null)
            {
                emailCollections.Add(await GetByEmailAddress(emailAddressSubstring));
            }
            if (firstname != null)
            {
                emailCollections.Add(await GetByFirstname(firstname));
            }
            if (lastname != null)
            {
                emailCollections.Add(await GetByLastname(lastname));
            }
            if (middlename != null)
            {
                emailCollections.Add(await GetByMiddlename(middlename));
            }
            if (!emailCollections.Any())
            {
                return new List<Email>();
            }
            return emailCollections.Aggregate((a, b) => a.Intersect(b));
        }
    }
}

