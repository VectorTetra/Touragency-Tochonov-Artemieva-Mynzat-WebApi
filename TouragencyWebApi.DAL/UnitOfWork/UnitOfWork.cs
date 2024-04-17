using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Repositories;
using TouragencyWebApi.DAL.Entities;

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
        private ISettlementsRepository  _settlements;
        private ITourStateRepository _statuses;
        private ITouragencyAccountRepository _account;
        private ITouragencyAccountRoleRepository _role;
        private ITouragencyEmployeeRepository _employee;

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
        public ISettlementsRepository Settlements
        {
            get
            {
                if(_settlements == null)
                    _settlements = new SettlementsRepository(_context);
                return _settlements;
            }
        }
        public ITourStateRepository TourStates
        {
            get
            {
                if (_statuses == null)
                    _statuses = new TourStateRepository(_context);
                return _statuses;
            }
        }
        public ITouragencyEmployeeRepository TouragencyEmployees
        {
            get
            {
                if (_employee == null)
                    _employee = new TouragencyEmployeeRepository(_context);
                return _employee;
            }
        }
        public ITouragencyAccountRoleRepository TouragencyAccountRoles
        {
            get
            {
                if(_role == null)
                    _role = new TouragencyAccountRoleRepository(_context);
                return _role;
            }
        }
        public ITouragencyAccountRepository TouragencyAccounts
        {
            get
            {
                if(_account == null)
                    _account = new TouragencyAccountRepository(_context);
                return _account;
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
