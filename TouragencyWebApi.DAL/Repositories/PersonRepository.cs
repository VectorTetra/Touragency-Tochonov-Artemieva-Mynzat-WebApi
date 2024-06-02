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
    public class PersonRepository : IPersonRepository
    {
        private readonly TouragencyContext _context;
        public PersonRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }
        public async Task<IEnumerable<Person>> Get200Last()
        {
            return await _context.Persons.OrderByDescending(p => p.Id).Take(200).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByIds(ICollection<int> ids)
        {
            return await _context.Persons.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByPhoneNumberSubstring(string phoneNumberSubstring)
        {
            return await _context.Persons.Where(p => p.Phones.Any(p => p.PhoneNumber.Contains(phoneNumberSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByEmailAddressSubstring(string emailAddressSubstring)
        {
            return await _context.Persons.Where(p => p.Emails.Any(p => p.EmailAddress.Contains(emailAddressSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByFirstnameSubstring(string firstname)
        {
            return await _context.Persons.Where(p => p.Firstname.Contains(firstname)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByLastnameSubstring(string lastname)
        {
            return await _context.Persons.Where(p => p.Lastname.Contains(lastname)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetByMiddlenameSubstring(string middlename)
        {
            return await _context.Persons.Where(p => p.Middlename.Contains(middlename)).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByCompositeSearch(ICollection<int>? ids, string? firstnameSubstring,
            string? lastnameSubstring, string? middlenameSubstring, string? phoneNumberSubstring, string? emailAddressSubstring)
        {
            var personCollections = new List<IEnumerable<Person>>();

            if (ids != null)
            {
                // Зробити вибірку по ids і додати результат в personCollections
                var personsByIds = await GetByIds(ids);
                personCollections.Add(personsByIds);
            }
            if (!string.IsNullOrEmpty(firstnameSubstring))
            {
                var personsByFirstname = await GetByFirstnameSubstring(firstnameSubstring);
                personCollections.Add(personsByFirstname);
            }

            if (!string.IsNullOrEmpty(lastnameSubstring))
            {
                var personsByLastname = await GetByLastnameSubstring(lastnameSubstring);
                personCollections.Add(personsByLastname);
            }

            if (!string.IsNullOrEmpty(middlenameSubstring))
            {
                var personsByMiddlename = await GetByMiddlenameSubstring(middlenameSubstring);
                personCollections.Add(personsByMiddlename);
            }

            if (!string.IsNullOrEmpty(phoneNumberSubstring))
            {
                var personsByPhoneNumber = await GetByPhoneNumberSubstring(phoneNumberSubstring);
                personCollections.Add(personsByPhoneNumber);
            }

            if (!string.IsNullOrEmpty(emailAddressSubstring))
            {
                var personsByEmailAddress = await GetByEmailAddressSubstring(emailAddressSubstring);
                personCollections.Add(personsByEmailAddress);
            }
            if (!personCollections.Any())
            {
                return new List<Person>();
            }
            return personCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task<Person?> GetById(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Person?> GetByClientId(int clientId)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Client.Id == clientId);
        }
        public async Task<Person?> GetByTouragencyEmployeeId(int touragencyEmployeeId)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.TouragencyEmployee.Id == touragencyEmployeeId);
        }
        public async Task Create(Person person)
        {
            await _context.Persons.AddAsync(person);
        }
        public void Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Person? person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
        }
    }
}
