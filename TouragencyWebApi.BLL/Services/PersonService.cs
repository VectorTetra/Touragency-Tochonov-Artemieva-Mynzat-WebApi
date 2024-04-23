using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class PersonService : IPersonService
    {
        IUnitOfWork Database;

        MapperConfiguration Person_PersonDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Firstname", opt => opt.MapFrom(c => c.Firstname))
        .ForMember("Lastname", opt => opt.MapFrom(c => c.Lastname))
        .ForMember("Middlename", opt => opt.MapFrom(c => c.Middlename))
        .ForPath(d => d.PhoneIds, opt => opt.MapFrom(c => c.Phones.Select(b => b.Id)))
        .ForPath(d => d.EmailIds, opt => opt.MapFrom(c => c.Emails.Select(b => b.Id)))
        .ForPath(d => d.ClientId, opt => opt.MapFrom(c => c.Client.Id))
        .ForPath(d => d.TouragencyEmployeeId, opt => opt.MapFrom(c => c.TouragencyEmployee.Id))
        );
        public PersonService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<PersonDTO> Create(PersonDTO personDTO)
        {
            var isExist = await Database.Persons.GetById(personDTO.Id);
            if (isExist != null)
            {
                throw new Exception($"Людина з таким id {personDTO.Id} вже існує");
            }
            var person = new Person
            {
                Id = personDTO.Id,
                Firstname = personDTO.Firstname,
                Lastname = personDTO.Lastname,
                Middlename = personDTO.Middlename,
                Phones = new List<Phone>(),
                Emails = new List<Email>()
            };
            if(personDTO.ClientId != null)
            {
                var client = await Database.Clients.GetById((int)personDTO.ClientId);
                if (client == null)
                {
                    throw new Exception($"Клієнта з таким id {personDTO.ClientId} не знайдено");
                }
                person.Client = client;
            }
            if (personDTO.TouragencyEmployeeId != null)
            {
                var touragencyEmployee = await Database.TouragencyEmployees.GetById((int)personDTO.TouragencyEmployeeId);
                if (touragencyEmployee == null)
                {
                    throw new Exception($"Співробітника турагенції з таким id {personDTO.TouragencyEmployeeId} не знайдено");
                }
                person.TouragencyEmployee = touragencyEmployee;
            }
            if (personDTO.PhoneIds != null)
            {
                foreach (var phoneId in personDTO.PhoneIds)
                {
                    var phone = await Database.Phones.GetById(phoneId);
                    if (phone == null)
                    {
                        throw new Exception($"Телефону з таким id {phoneId} не знайдено");
                    }
                    person.Phones.Add(phone);
                }
            }
            if (personDTO.EmailIds != null)
            {
                foreach (var emailId in personDTO.EmailIds)
                {
                    var email = await Database.Emails.GetById(emailId);
                    if (email == null)
                    {
                        throw new Exception($"Електронної пошти з таким id {emailId} не знайдено");
                    }
                    person.Emails.Add(email);
                }
            }
            await Database.Persons.Create(person);
            await Database.Save();
            personDTO.Id = person.Id;
            return personDTO;
        }
        public async Task<PersonDTO> Update(PersonDTO personDTO)
        {
            var person = await Database.Persons.GetById(personDTO.Id);
            if (person == null)
            {
                throw new Exception($"Людину з таким id {personDTO.Id} не знайдено");
            }
            person.Firstname = personDTO.Firstname;
            person.Lastname = personDTO.Lastname;
            person.Middlename = personDTO.Middlename;
            if (personDTO.ClientId != null)
            {
                var client = await Database.Clients.GetById((int)personDTO.ClientId);
                if (client == null)
                {
                    throw new Exception($"Клієнта з таким id {personDTO.ClientId} не знайдено");
                }
                person.Client = client;
            }
            if (personDTO.TouragencyEmployeeId != null)
            {
                var touragencyEmployee = await Database.TouragencyEmployees.GetById((int)personDTO.TouragencyEmployeeId);
                if (touragencyEmployee == null)
                {
                    throw new Exception($"Співробітника турагенції з таким id {personDTO.TouragencyEmployeeId} не знайдено");
                }
                person.TouragencyEmployee = touragencyEmployee;
            }
            if (personDTO.PhoneIds != null)
            {
                person.Phones.Clear();
                foreach (var phoneId in personDTO.PhoneIds)
                {
                    var phone = await Database.Phones.GetById(phoneId);
                    if (phone == null)
                    {
                        throw new Exception($"Телефону з таким id {phoneId} не знайдено");
                    }
                    person.Phones.Add(phone);
                }
            }
            if (personDTO.EmailIds != null)
            {
                person.Emails.Clear();
                foreach (var emailId in personDTO.EmailIds)
                {
                    var email = await Database.Emails.GetById(emailId);
                    if (email == null)
                    {
                        throw new Exception($"Електронної пошти з таким id {emailId} не знайдено");
                    }
                    person.Emails.Add(email);
                }
            }
            Database.Persons.Update(person);
            await Database.Save();
            return personDTO;
        }
        public async Task<PersonDTO> Delete(int id)
        {
            var person = await Database.Persons.GetById(id);
            if (person == null)
            {
                throw new Exception($"Людину з таким id {id} не знайдено");
            }
            var dto = await GetById(id);
            await Database.Persons.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<PersonDTO>> GetAll()
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetAll());
        }

        public async Task<IEnumerable<PersonDTO>> Get200Last()
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.Get200Last());
        }

        public async Task<PersonDTO> GetByClientId(int clientId)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<Person, PersonDTO>(await Database.Persons.GetByClientId(clientId));
        }

        public async Task<IEnumerable<PersonDTO>> GetByCompositeSearch(ICollection<int>? ids, string? firstnameSubstring, string? lastnameSubstring, string? middlenameSubstring, string? phoneNumberSubstring, string? emailAddressSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByCompositeSearch(ids, firstnameSubstring, lastnameSubstring, middlenameSubstring, phoneNumberSubstring, emailAddressSubstring));

        }

        public async Task<IEnumerable<PersonDTO>> GetByEmailAddressSubstring(string emailAddressSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByEmailAddressSubstring(emailAddressSubstring));
        }

        public async Task<IEnumerable<PersonDTO>> GetByFirstnameSubstring(string firstnameSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByFirstnameSubstring(firstnameSubstring));

        }

        public async Task<PersonDTO> GetById(int id)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<Person, PersonDTO>(await Database.Persons.GetById(id));
        }

        public async Task<IEnumerable<PersonDTO>> GetByIds(ICollection<int> ids)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByIds(ids));
        }

        public async Task<IEnumerable<PersonDTO>> GetByLastnameSubstring(string lastnameSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByLastnameSubstring(lastnameSubstring));
        }

        public async Task<IEnumerable<PersonDTO>> GetByMiddlenameSubstring(string middlenameSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByMiddlenameSubstring(middlenameSubstring));
        }

        public async Task<IEnumerable<PersonDTO>> GetByPhoneNumberSubstring(string phoneNumberSubstring)
        {
            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(await Database.Persons.GetByPhoneNumberSubstring(phoneNumberSubstring));
        }

        public async Task<PersonDTO> GetByTouragencyEmployeeId(int touragencyEmployeeId)
        {

            var mapper = new Mapper(Person_PersonDTOMapConfig);
            return mapper.Map<Person, PersonDTO>(await Database.Persons.GetByTouragencyEmployeeId(touragencyEmployeeId));
        }

        
    }
}
