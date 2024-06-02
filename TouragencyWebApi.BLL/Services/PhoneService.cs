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
    public class PhoneService: IPhoneService
    {
        IUnitOfWork Database;

        MapperConfiguration Phone_PhoneDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Phone, PhoneDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("PhoneNumber", opt => opt.MapFrom(c => c.PhoneNumber))
        .ForMember("ContactTypeId", opt => opt.MapFrom(c => c.ContactTypeId))
        .ForPath(d => d.PersonIds, opt => opt.MapFrom(c => c.Persons.Select(b => b.Id)))
        );
        public PhoneService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<PhoneDTO> TryToAddNewPhone(PhoneDTO phoneDTO)
        {
            var BusyPhone = await Database.Phones.GetByPhoneNumber(phoneDTO.PhoneNumber);
            if (BusyPhone.Any(ph => ph.PhoneNumber == phoneDTO.PhoneNumber))
            {
                throw new ValidationException("Такий номер телефону вже зайнято!", "");
            }
            var newPhone = new Phone
            {
                PhoneNumber = phoneDTO.PhoneNumber,
                ContactTypeId = phoneDTO.ContactTypeId,
                Persons = new List<Person>()
            };

            foreach (var id in phoneDTO.PersonIds)
            {
                var person = await Database.Persons.GetById(id);
                if (person != null)
                {
                    newPhone.Persons.Add(person);
                }
            }
            await Database.Phones.Create(newPhone);
            await Database.Save();
            phoneDTO.Id = newPhone.Id;
            return phoneDTO;
        }

        public async Task<PhoneDTO> Update(PhoneDTO phoneDTO)
        {
            Phone phone = await Database.Phones.GetById(phoneDTO.Id);
            if (phone == null)
            {
                throw new ValidationException("Телефон не знайдено", "");
            }
            phone.PhoneNumber = phoneDTO.PhoneNumber;
            phone.ContactTypeId = phoneDTO.ContactTypeId;
            phone.Persons.Clear();
            foreach (var id in phoneDTO.PersonIds)
            {
                var person = await Database.Persons.GetById(id);
                if (person != null)
                {
                    phone.Persons.Add(person);
                }
            }
            Database.Phones.Update(phone);
            await Database.Save();
            return phoneDTO;
        }

        public async Task<PhoneDTO> Delete(long id)
        {
            
            Phone phone = await Database.Phones.GetById(id);
            if (phone == null)
            {
                throw new ValidationException("Телефон не знайдено", "");
            }
            var dto = await GetById(id);
            await Database.Phones.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<PhoneDTO>> GetAll()
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetAll());
        }

        public async Task<IEnumerable<PhoneDTO>> Get200Last()
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.Get200Last());
        }

        public async Task<PhoneDTO?> GetById(long id)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<Phone, PhoneDTO>(await Database.Phones.GetById(id));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByClientId(int clientId)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByClientId(clientId));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByPersonId(int personId)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByPersonId(personId));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByTouragencyEmployeeId(touragencyEmployeeId));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByContactTypeId(int contactTypeId)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByContactTypeId(contactTypeId));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByPhoneNumber(string phoneNumberSubstring)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByPhoneNumber(phoneNumberSubstring));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByFirstname(string firstname)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByFirstname(firstname));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByLastname(string lastname)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByLastname(lastname));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByMiddlename(string middlename)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByMiddlename(middlename));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByTouristNickname(string touristNickname)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByTouristNickname(touristNickname));
        }

        public async Task<IEnumerable<PhoneDTO>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
                       string? touristNickname, int? contactTypeId, string? phoneNumberSubstring, string? firstname,
                                  string? lastname, string? middlename)
        {
            var mapper = new Mapper(Phone_PhoneDTOMapConfig);
            return mapper.Map<IEnumerable<Phone>, IEnumerable<PhoneDTO>>(await Database.Phones.GetByCompositeSearch(clientId, personId, touragencyEmployeeId,
                               touristNickname, contactTypeId, phoneNumberSubstring, firstname, lastname, middlename));
        }
    }
}
