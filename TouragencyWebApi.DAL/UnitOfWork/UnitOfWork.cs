using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Repositories;

namespace TouragencyWebApi.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TouragencyContext _context;
        private IClientRepository _clients;
        private IEmailRepository _emails;
        private IPhoneRepository _phones;
        private IPersonRepository _persons;
        private ICountriesRepository _countries;

        public UnitOfWork(TouragencyContext context)
        {
            _context = context;
        }

        public IPersonRepository Persons
        {
            get
            {
                if (_persons == null)
                    _persons = new PersonRepository(_context);
                return _persons;
            }
        }
        public IClientRepository Clients
        {
            get
            {
                if (_clients == null)
                    _clients = new ClientRepository(_context);
                return _clients;
            }
        }
        public IEmailRepository Emails
        {
            get
            {
                if (_emails == null)
                    _emails = new EmailRepository(_context);
                return _emails;
            }
        }
        public IPhoneRepository Phones
        {
            get
            {
                if (_phones == null)
                    _phones = new PhoneRepository(_context);
                return _phones;
            }
        }
        public ICountriesRepository Countries
        {
            get
            {
                if (_countries == null)
                    _countries = new CountriesRepository(_context);
                return _countries;
            }
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
