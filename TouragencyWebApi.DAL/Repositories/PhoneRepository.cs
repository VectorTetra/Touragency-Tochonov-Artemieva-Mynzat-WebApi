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
            _context.Phones.ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByClientId(int clientId)
        {
            _context.Phones.Where(p => p.ClientId == clientId).ToListAsync();
        }
        public async Task<IEnumerable<Phone>> GetByPersonId(int personId) { }
        public async Task<IEnumerable<Phone>> GetByTouragencyEmployeeId(int touragencyEmployeeId) { }
        public async Task<IEnumerable<Phone>> GetByTouristNickname(string touristNickname) { }
        public async Task<IEnumerable<Phone>> GetByContactTypeId(string contactTypeId) { }
        public async Task<Phone> GetByPhoneNumber(string phoneNumber) { }
        public async Task Create(Phone phone) { }
        public void Update(Phone phone) { }
        public async Task Delete(int id) { }
    }
}

