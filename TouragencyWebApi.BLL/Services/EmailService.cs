using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DAL.Interfaces;
using AutoMapper;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Services
{
    public class EmailService: IEmailService
    {
        IUnitOfWork Database;

        MapperConfiguration Email_EmailDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Email, EmailDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("EmailAddress", opt => opt.MapFrom(c => c.EmailAddress))
        .ForMember("ContactTypeId", opt => opt.MapFrom(c => c.ContactTypeId))
        .ForPath(d => d.PersonIds, opt => opt.MapFrom(c => c.Persons.Select(b => b.Id)))
        );
        public EmailService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task TryToAddNewEmail(EmailDTO emailDTO)
        {
            var BusyEmail= await Database.Emails.GetByEmailAddress(emailDTO.EmailAddress);
            if (BusyEmail.Any(em => em.EmailAddress == emailDTO.EmailAddress))
            {
                throw new ValidationException("Такий email вже зайнято!", "");
            }
            var newEmail = new Email
            {
                EmailAddress = emailDTO.EmailAddress,
                ContactTypeId = emailDTO.ContactTypeId,
                Persons = new List<Person>()
            };

            foreach (var id in emailDTO.PersonIds)
            {
                var person = await Database.Persons.GetById(id);
                if (person != null)
                {
                    newEmail.Persons.Add(person);
                }
            }
            await Database.Emails.Create(newEmail);
            await Database.Save();
        }

        public async Task Update(EmailDTO emailDTO)
        {
            Email email = await Database.Emails.GetById(emailDTO.Id);
            if (email == null)
            {
                throw new ValidationException("Email не знайдено", "");
            }
            email.EmailAddress = emailDTO.EmailAddress;
            email.ContactTypeId = emailDTO.ContactTypeId;
            email.Persons.Clear();
            foreach (var id in emailDTO.PersonIds)
            {
                var person = await Database.Persons.GetById(id);
                if (person != null)
                {
                    email.Persons.Add(person);
                }
            }
            Database.Emails.Update(email);
            await Database.Save();
        }

        public async Task Delete(long id)
        {
            Email email = await Database.Emails.GetById(id);
            if (email == null)
            {
                throw new ValidationException("Email не знайдено", "");
            }
            await Database.Emails.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<EmailDTO>> GetAll()
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetAll());
        }

        public async Task<EmailDTO?> GetById(long id)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<Email, EmailDTO>(await Database.Emails.GetById(id));
        }

        public async Task<IEnumerable<EmailDTO>> GetByClientId(int clientId)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetByClientId(clientId));
        }

        public async Task<IEnumerable<EmailDTO>> GetByPersonId(int personId)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetByPersonId(personId));
        }

        public async Task<IEnumerable<EmailDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetByTouragencyEmployeeId(touragencyEmployeeId));
        }

        public async Task<IEnumerable<EmailDTO>> GetByContactTypeId(int contactTypeId)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetByContactTypeId(contactTypeId));
        }

        public async Task<IEnumerable<EmailDTO>> GetByEmailAddress(string emailAddressSubstring)
        {
            var mapper = new Mapper(Email_EmailDTOMapConfig);
            return mapper.Map<IEnumerable<Email>, IEnumerable<EmailDTO>>(await Database.Emails.GetByEmailAddress(emailAddressSubstring));
        }
    }
}
