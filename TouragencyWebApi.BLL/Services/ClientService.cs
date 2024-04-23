using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DTO;
using TouragencyWebApi.DAL.Entities;
using AutoMapper;
namespace TouragencyWebApi.BLL.Services
{
    public class ClientService : IClientService
    {
        IUnitOfWork Database;

        MapperConfiguration Client_ClientDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("TouristNickname", opt => opt.MapFrom(c => c.TouristNickname))
        .ForMember("AvatarImagePath", opt => opt.MapFrom(c => c.AvatarImagePath))
        .ForMember("TouragencyAccountRoleId", opt => opt.MapFrom(c => c.TouragencyAccountRoleId))
        .ForPath(d => d.Person.Firstname, opt => opt.MapFrom(c => c.Person.Firstname))
        .ForPath(d => d.Person.Lastname, opt => opt.MapFrom(c => c.Person.Lastname))
        .ForPath(d => d.Person.Middlename, opt => opt.MapFrom(c => c.Person.Middlename))
        .ForPath(d => d.Person.PhoneIds, opt => opt.MapFrom(c => c.Person.Phones.Select(ph => ph.Id)))
        .ForPath(d => d.Person.EmailIds, opt => opt.MapFrom(c => c.Person.Emails.Select(em => em.Id)))
        .ForPath(d => d.BookingIds, opt => opt.MapFrom(c => c.Bookings.Select(b => b.Id)))
        .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        .ForPath(d => d.ReviewIds, opt => opt.MapFrom(c => c.Reviews.Select(b => b.Id)))
        );
        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<ClientDTO> TryToRegister(ClientRegisterDTO regDto)
        {
            //Перш ніж зареєструватись, треба перевірити, чи є такий нік туриста в БД
            var BusyLoginClientsCollection = await Database.Clients.GetByTouristNickname(regDto.TouristNickname);
            if (BusyLoginClientsCollection.ToList().Any(x => x.TouristNickname == regDto.TouristNickname))
            {
                throw new ValidationException($"Такий нік туриста вже зайнято! (regDto.TouristNickname : {regDto.TouristNickname})", "");
            }
            // Також треба перевірити, чи паролі співпадають
            if (regDto.Password != regDto.PasswordConfirm)
            {
                throw new ValidationException("Паролі не співпадають!", "");
            }
            var isPhoneExist = await Database.Phones.GetByPhoneNumber(regDto.Phone);
            if (isPhoneExist.ToList().Count != 0)
            {
                throw new ValidationException("Такий телефон вже зареєстровано!", "");
            }
            var isEmailExist = await Database.Emails.GetByEmailAddress(regDto.Email);
            if (isEmailExist.ToList().Count != 0)
            {
                throw new ValidationException("Такий email вже зареєстровано!", "");
            }
            try
            {
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + regDto.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                var newClient = new Client
                {
                    TouristNickname = regDto.TouristNickname,
                    Password = hash.ToString(),
                    Salt = salt,
                    Person = new Person
                    {
                        Lastname = regDto.Lastname,
                        Firstname = regDto.Firstname,
                        Middlename = regDto.Middlename,
                        Phones = new List<Phone> { new Phone { PhoneNumber = regDto.Phone, ContactTypeId = 1 } },
                        Emails = new List<Email> { new Email { EmailAddress = regDto.Email, ContactTypeId = 3 } }
                    }
                };
                await Database.Clients.Create(newClient);
                await Database.Save();
                var mapper = new Mapper(Client_ClientDTOMapConfig);
                var clDto = mapper.Map<Client, ClientDTO>(newClient);
                return clDto;
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message, "");
            }

        }
        public async Task<ClientDTO> TryToLogin(ClientLoginDTO loginDto)
        {
            if (loginDto.Email == null && loginDto.Phone == null && loginDto.TouristNickname == null)
            {
                throw new ValidationException("Не вказано нік туриста, або телефон, або email", "");
            }
            Client? MeaningUser = null;
            // Якщо в БД немає користувачів, то показати неоднозначну помилку
            var UsersCollection = await Database.Clients.GetAll();
            if (UsersCollection.ToList().Count == 0)
            {
                throw new ValidationException("Неправильний нік туриста або пароль", "");
            }
            // Якщо відбувається автентифікація туриста по ніку
            if (loginDto.TouristNickname != null)
            {
                // Якщо в БД немає користувачів з подібним ніком туриста, то показати неоднозначну помилку
                var SimilarClients = await Database.Clients.GetByTouristNickname(loginDto.TouristNickname);
                if (SimilarClients.ToList().Count == 0)
                {
                    throw new ValidationException("Неправильний нік туриста або пароль", "");
                }
                else
                {
                    MeaningUser = SimilarClients.FirstOrDefault(x => x.TouristNickname == loginDto.TouristNickname);
                    // Якщо в БД немає користувача з таким конкретним ніком туриста, то показати неоднозначну помилку
                    if (MeaningUser == null)
                    {
                        throw new ValidationException("Неправильний нік туриста або пароль", "");
                    }
                }
            }
            // Якщо відбувається автентифікація туриста по email
            if (loginDto.Email != null)
            {
                // Якщо в БД немає користувачів з подібним email, то показати неоднозначну помилку
                var SimilarClients = await Database.Clients.GetByEmailAddress(loginDto.Email);
                if (SimilarClients.ToList().Count == 0)
                {
                    throw new ValidationException("Неправильний email або пароль", "");
                }
                else
                {
                    MeaningUser = SimilarClients.FirstOrDefault(x => x.Person.Emails.Any(em => em.EmailAddress == loginDto.Email));
                    // Якщо в БД немає користувача з таким конкретним email, то показати неоднозначну помилку
                    if (MeaningUser == null)
                    {
                        throw new ValidationException("Неправильний email або пароль", "");
                    }
                }
            }
            // Якщо відбувається автентифікація туриста по телефону
            if (loginDto.Phone != null)
            {
                // Якщо в БД немає користувачів з подібним телефоном, то показати неоднозначну помилку
                var SimilarClients = await Database.Clients.GetByPhoneNumber(loginDto.Phone);
                if (SimilarClients.ToList().Count == 0)
                {
                    throw new ValidationException("Неправильний телефон або пароль", "");
                }
                else
                {
                    MeaningUser = SimilarClients.FirstOrDefault(x => x.Person.Phones.Any(ph => ph.PhoneNumber == loginDto.Phone));
                    // Якщо в БД немає користувача з таким конкретним телефоном, то показати неоднозначну помилку
                    if (MeaningUser == null)
                    {
                        throw new ValidationException("Неправильний телефон або пароль", "");
                    }
                }
            }
            if (MeaningUser != null)
            {
                string? salt = MeaningUser.Salt;
                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + loginDto.Password);
                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();
                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));
                // Якщо паролі не співпадають, то показати неоднозначну помилку
                if (MeaningUser.Password != hash.ToString())
                {
                    throw new ValidationException("Неправильний логін або пароль", "");
                }
                var phoneIdsCollec = MeaningUser.Person.Phones.Select(ph => ph.Id);
                var emailIdsCollec = MeaningUser.Person.Emails.Select(em => em.Id);
                return new ClientDTO
                {
                    Id = MeaningUser.Id,
                    TouristNickname = MeaningUser.TouristNickname,
                    Person = new PersonDTO
                    {
                        Id = MeaningUser.Person.Id,
                        Lastname = MeaningUser.Person.Lastname,
                        Firstname = MeaningUser.Person.Firstname,
                        Middlename = MeaningUser.Person.Middlename,
                        PhoneIds = phoneIdsCollec.ToList(),
                        EmailIds = emailIdsCollec.ToList(),
                        ClientId = MeaningUser.Id
                    },
                    BookingIds = MeaningUser.Bookings.Select(b => b.Id).ToList(),
                    ReviewIds = MeaningUser.Reviews.Select(r => r.Id).ToList(),
                    TourIds = MeaningUser.Tours.Select(t => t.Id).ToList(),
                    TouragencyAccountRoleId = MeaningUser.TouragencyAccountRoleId,
                    AvatarImagePath = MeaningUser.AvatarImagePath
                };
            }
            throw new ValidationException("Вказано неправильні дані", "");

        }
        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetAll());
        }

        public async Task<IEnumerable<ClientDTO>> Get200Last()
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.Get200Last());
        }
        public async Task<ClientDTO?> GetById(int clientId)
        {
            Client? MeaningUser = await Database.Clients.GetById(clientId);
            if (MeaningUser == null)
            {
                return null;
            }
            return new ClientDTO
            {
                Id = MeaningUser.Id,
                TouristNickname = MeaningUser.TouristNickname,
                Person = new PersonDTO
                {
                    Id = MeaningUser.Person.Id,
                    Lastname = MeaningUser.Person.Lastname,
                    Firstname = MeaningUser.Person.Firstname,
                    Middlename = MeaningUser.Person.Middlename,
                    PhoneIds = MeaningUser.Person.Phones.Select(ph => ph.Id).ToList(),
                    EmailIds = MeaningUser.Person.Emails.Select(em => em.Id).ToList()
                },
                BookingIds = MeaningUser.Bookings.Select(b => b.Id).ToList(),
                AvatarImagePath = MeaningUser.AvatarImagePath
            };
        }
        public async Task<ClientDTO?> GetByPersonId(int personId)
        {
            Client? MeaningUser = await Database.Clients.GetByPersonId(personId);
            if (MeaningUser == null)
            {
                return null;
            }
            return new ClientDTO
            {
                Id = MeaningUser.Id,
                TouristNickname = MeaningUser.TouristNickname,
                Person = new PersonDTO
                {
                    Id = MeaningUser.Person.Id,
                    Lastname = MeaningUser.Person.Lastname,
                    Firstname = MeaningUser.Person.Firstname,
                    Middlename = MeaningUser.Person.Middlename,
                    PhoneIds = MeaningUser.Person.Phones.Select(ph => ph.Id).ToList(),
                    EmailIds = MeaningUser.Person.Emails.Select(em => em.Id).ToList()
                },
                BookingIds = MeaningUser.Bookings.Select(b => b.Id).ToList(),
                AvatarImagePath = MeaningUser.AvatarImagePath
            };
        }
        public async Task<ClientDTO?> GetByBookingId(long bookingId)
        {
            Client? MeaningUser = await Database.Clients.GetByBookingId(bookingId);
            if (MeaningUser == null)
            {
                return null;
            }
            return new ClientDTO
            {
                Id = MeaningUser.Id,
                TouristNickname = MeaningUser.TouristNickname,
                Person = new PersonDTO
                {
                    Id = MeaningUser.Person.Id,
                    Lastname = MeaningUser.Person.Lastname,
                    Firstname = MeaningUser.Person.Firstname,
                    Middlename = MeaningUser.Person.Middlename,
                    PhoneIds = MeaningUser.Person.Phones.Select(ph => ph.Id).ToList(),
                    EmailIds = MeaningUser.Person.Emails.Select(em => em.Id).ToList()
                },
                BookingIds = MeaningUser.Bookings.Select(b => b.Id).ToList(),
                AvatarImagePath = MeaningUser.AvatarImagePath
            };
        }
        public async Task<IEnumerable<ClientDTO>> GetByTouristNickname(string touristNickname)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByTouristNickname(touristNickname));
        }
        public async Task<IEnumerable<ClientDTO>> GetByFirstname(string firstname)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByFirstname(firstname));
        }
        public async Task<IEnumerable<ClientDTO>> GetByLastname(string lastname)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByLastname(lastname));
        }
        public async Task<IEnumerable<ClientDTO>> GetByMiddlename(string middlename)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByMiddlename(middlename));
        }

        public async Task<IEnumerable<ClientDTO>> GetByPhoneNumber(string phoneNumber)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByPhoneNumber(phoneNumber));
        }
        public async Task<IEnumerable<ClientDTO>> GetByEmailAddress(string emailAddress)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByEmailAddress(emailAddress));
        }
        public async Task<IEnumerable<ClientDTO>> GetByCompositeSearch(string? touristNickname, string? emailAddress,
                       string? phoneNumber, string? firstname, string? lastname, string? middlename)
        {
            var mapper = new Mapper(Client_ClientDTOMapConfig);
            return mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await Database.Clients.GetByCompositeSearch(touristNickname, emailAddress, phoneNumber, firstname, lastname, middlename));
        }
        public async Task<ClientDTO> Update(ClientDTO clientDTO)
        {
            var client = await Database.Clients.GetById(clientDTO.Id);
            if (client == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }

            else
            {
                client.Person.Phones.Clear();
                foreach (var id in clientDTO.Person.PhoneIds)
                {
                    var phone = await Database.Phones.GetById(id);
                    if (phone == null)
                    {
                        throw new ValidationException("Такого телефону не існує!", "");
                    }
                    client.Person.Phones.Add(phone);
                }
                client.Person.Emails.Clear();
                foreach (var id in clientDTO.Person.EmailIds)
                {
                    var email = await Database.Emails.GetById(id);
                    if (email == null)
                    {
                        throw new ValidationException("Такого email не існує!", "");
                    }
                    client.Person.Emails.Add(email);
                }
                client.AvatarImagePath = clientDTO.AvatarImagePath;
                client.TouristNickname = clientDTO.TouristNickname;
                client.Person.Firstname = clientDTO.Person.Firstname;
                client.Person.Lastname = clientDTO.Person.Lastname;
                client.Person.Middlename = clientDTO.Person.Middlename;
                Database.Clients.Update(client);
                await Database.Save();
                return clientDTO;
            }
        }
        public async Task<ClientDTO> Delete(int id)
        {
            var User = await Database.Clients.GetById(id);
            if (User == null)
            {
                throw new ValidationException($"Такого користувача з вказаним id не існує! (id : {id})", "");
            }
            var dto = await GetById(id);
            await Database.Clients.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
