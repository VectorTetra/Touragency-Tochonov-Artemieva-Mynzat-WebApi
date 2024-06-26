﻿using System;
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
    public class ClientRepository : IClientRepository
    {
        private readonly TouragencyContext _context;
        public ClientRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Client>> Get200Last()
        {
            return await _context.Clients.OrderByDescending(c => c.Id).Take(200).ToListAsync();
        }

        public async Task<Client?> GetById(int clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<Client?> GetByPersonId(int personId)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.PersonId == personId);
        }

        public async Task<Client?> GetByBookingId(long bookingId)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Bookings.Any(b => b.Id == bookingId));
        }

        public async Task<IEnumerable<Client>> GetByFirstname(string firstname)
        {
            return await _context.Clients.Where(c => c.Person.Firstname.Contains(firstname)).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByLastname(string lastname)
        {
            return await _context.Clients.Where(c => c.Person.Lastname.Contains(lastname)).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByMiddlename(string middlename)
        {
            return await _context.Clients.Where(c => c.Person.Lastname.Contains(middlename)).ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetByTouristNickname(string touristNickname)
        {
            return await _context.Clients.Where(c => c.TouristNickname.Contains(touristNickname)).ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetByEmailAddress(string emailAddress)
        {
            return await _context.Clients.Where(c => c.Person.Emails.Any(e => e.EmailAddress.Contains(emailAddress))).ToListAsync();
        }
        public async Task<IEnumerable<Client>> GetByPhoneNumber(string phoneNumber)
        {
            return await _context.Clients.Where(c => c.Person.Phones.Any(p => p.PhoneNumber.Contains(phoneNumber))).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByCompositeSearch(string? touristNickname, string? emailAddress,
            string? phoneNumber, string? firstname, string? lastname, string? middlename)
        {
            var clientCollections = new List<IEnumerable<Client>>();

            if (touristNickname != null)
            {
                clientCollections.Add(await GetByTouristNickname(touristNickname));
            }
            if (emailAddress != null)
            {
                clientCollections.Add(await GetByEmailAddress(emailAddress));
            }
            if (phoneNumber != null)
            {
                clientCollections.Add(await GetByPhoneNumber(phoneNumber));
            }
            if (firstname != null)
            {
                clientCollections.Add(await GetByFirstname(firstname));
            }
            if (lastname != null)
            {
                clientCollections.Add(await GetByLastname(lastname));
            }
            if (middlename != null)
            {
                clientCollections.Add(await GetByMiddlename(middlename));
            }

            if (!clientCollections.Any())
            {
                return new List<Client>();
            }
            return clientCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }

        public async Task Create(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public void Update(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Client? client = await _context.Clients.FindAsync(id);
            if (client != null)
                _context.Clients.Remove(client);
        }
        public async Task<IEnumerable<Phone>> GetPhones(int clientId) 
        {
            return await _context.Phones.Where(ph => ph.Persons.Any(pr=> pr.Client.Id == clientId)).ToListAsync();
        }
        public async Task<IEnumerable<Email>> GetEmails(int clientId) 
        {
            return await _context.Emails.Where(ph => ph.Persons.Any(pr => pr.Client.Id == clientId)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookings(int clientId)
        {
            return await _context.Bookings.Where(b => b.Client.Id == clientId).ToListAsync();
        }
    }
}
