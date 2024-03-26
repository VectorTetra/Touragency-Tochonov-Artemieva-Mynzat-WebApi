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
        public async Task<IEnumerable<Person>> GetAllByIds(ICollection<int> ids)
        {
            return await _context.Persons.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllByPhoneNumber(string phoneNumber)
        {
            return await _context.Persons.Where(p => p.Phones.Any(p => p.PhoneNumber == phoneNumber)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllByEmailAddress(string emailAddress)
        {
            return await _context.Persons.Where(p => p.Emails.Any(p => p.EmailAddress == emailAddress)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllByFirstname(string firstname)
        {
            return await _context.Persons.Where(p => p.Firstname.Contains(firstname)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllByLastname(string lastname)
        {
            return await _context.Persons.Where(p => p.Lastname.Contains(lastname)).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllByMiddleName(string middlename)
        {
            return await _context.Persons.Where(p => p.Middlename.Contains(middlename)).ToListAsync();
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
