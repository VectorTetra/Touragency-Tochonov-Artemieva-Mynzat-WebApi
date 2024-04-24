using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.BLL.Services
{
    public class TouragencyAccountService : ITouragencyAccountService
    {
        IUnitOfWork Database;

        MapperConfiguration Account_AccountDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TouragencyEmployeeAccount, TouragencyEmployeeAccountDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Login", opt => opt.MapFrom(c => c.Login))
        .ForMember("Password", opt => opt.MapFrom(c => c.Password))
        .ForMember("Salt", opt => opt.MapFrom(c => c.Salt))
        .ForMember("TouragencyAccountRoleId", opt => opt.MapFrom(c => c.TouragencyAccountRoleId))
        .ForMember("TouragencyEmployeeId", opt => opt.MapFrom(c => c.TouragencyEmployeeId))
        );

        public TouragencyAccountService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<TouragencyEmployeeAccountDTO> TryToRegister(TouragencyAccountRegisterDTO reg)
        {
            var TakenLogin = await Database.TouragencyAccounts.GetByLogin(reg.Login);
            if (TakenLogin.ToList().Any(x => x.Login == reg.Login))
            {
                throw new ValidationException("Такий логін вже зайнято!", "");
            }
            
            if (reg.Password != reg.PasswordConfirm)
            {
                throw new ValidationException("Паролі не співпадають!", "");
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
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                var touragencyEmployee = new TouragencyEmployee
                {
                    Person = new Person
                    {
                        Lastname = reg.Lastname,
                        Firstname = reg.Firstname,
                        Middlename = reg.Middlename,
                        Phones = new List<Phone> { new Phone { PhoneNumber = reg.Phone, ContactTypeId = 1 } },
                        Emails = new List<Email> { new Email { EmailAddress = reg.Email, ContactTypeId = 3 } }
                    },
                    Position = new Position
                    {
                        Name = reg.PositionName,
                        Description = reg.Description,
                    }
                };
                var newTouragencyAccount = new TouragencyEmployeeAccount
                {
                    Login = reg.Login,
                    Password = hash.ToString(),
                    Salt = salt,
                    TouragencyEmployee = touragencyEmployee,
                    TouragencyAccountRoleId = reg.TouragencyAccountRoleId 
                };

                await Database.TouragencyAccounts.Create(newTouragencyAccount);
                await Database.Save();
                var dto = await GetById(newTouragencyAccount.Id);
                return dto;
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message, "");
            }

        }

        public async Task<TouragencyEmployeeAccountDTO> TryToLogin(TouragencyAccountLoginDTO login)
        {
            if (login.Login == null)
            {
                throw new ValidationException("Не вказано логін", "");
            }
            TouragencyEmployeeAccount? MeaningUser = null;
            // Якщо в БД немає користувачів, то показати неоднозначну помилку
            var UsersCollection = await Database.TouragencyAccounts.GetAll();
            if (UsersCollection.ToList().Count == 0)
            {
                throw new ValidationException("Неправильний логін або пароль", "");
            }
            
            if (login.Login != null)
            {
                
                var SimilarEmployees = await Database.TouragencyAccounts.GetByLogin(login.Login);
                if (SimilarEmployees.ToList().Count == 0)
                {
                    throw new ValidationException("Неправильний логін або пароль", "");
                }
                else
                {
                    MeaningUser = SimilarEmployees.FirstOrDefault(x => x.Login == login.Login);
                    // Якщо в БД немає користувача з таким конкретним ніком туриста, то показати неоднозначну помилку
                    if (MeaningUser == null)
                    {
                        throw new ValidationException("Неправильний логін або пароль", "");
                    }
                }
            }
            if (MeaningUser != null)
            {
                string? salt = MeaningUser.Salt;
                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + login.Password);
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
                return new TouragencyEmployeeAccountDTO
                {
                    Id = MeaningUser.Id,
                    Login = MeaningUser.Login,
                    Password = hash.ToString(),
                    TouragencyAccountRoleId = MeaningUser.TouragencyAccountRoleId,
                    TouragencyEmployeeId = MeaningUser.TouragencyEmployeeId
                };
            }
            throw new ValidationException("Вказано неправильні дані", "");
        }

        public async Task<TouragencyEmployeeAccountDTO> Update(TouragencyEmployeeAccountDTO accountDTO)
        {
            TouragencyEmployeeAccount account = await Database.TouragencyAccounts.GetById(accountDTO.Id);
            if (account == null)
            {
                throw new ValidationException("Такий аккаунт вже існує", "");
            }
            account.Login = accountDTO.Login;
            account.Password = accountDTO.Password;
            account.TouragencyAccountRoleId = accountDTO.TouragencyAccountRoleId;
            account.TouragencyEmployeeId = accountDTO.TouragencyEmployeeId;

            var role = await Database.TouragencyAccountRoles.GetById(account.TouragencyAccountRoleId);
            account.TouragencyAccountRole = role;

            var employee = await Database.TouragencyEmployees.GetById(account.TouragencyEmployeeId);
            account.TouragencyEmployee = employee;

            Database.TouragencyAccounts.Update(account);
            await Database.Save();
            return accountDTO;
        }

        public async Task<TouragencyEmployeeAccountDTO> Delete(int id)
        {
            TouragencyEmployeeAccount account = await Database.TouragencyAccounts.GetById(id);
            if (account == null)
            {
                throw new ValidationException("Такий аккаунт вже існує", "");
            }
            var dto = await GetById(id);
            await Database.TouragencyAccounts.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetAll()
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetAll());
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> Get200Last()
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.Get200Last());
        }

        public async Task<TouragencyEmployeeAccountDTO?> GetById(int id)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<TouragencyEmployeeAccount, TouragencyEmployeeAccountDTO>(await Database.TouragencyAccounts.GetById(id));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByLogin(string login)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByLogin(login));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRoleName(string roleName)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByRoleName(roleName));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRoleDescription(string roleDescription)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByRoleDescription(roleDescription));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeFirstname(string employeeFirstname)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByEmployeeFirstname(employeeFirstname));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeLastname(string employeeLastname)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByEmployeeLastname(employeeLastname));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeMiddlename(string employeeMiddlename)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByEmployeeMiddlename(employeeMiddlename));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByCompositeSearch(string? login, string? roleName, string? roleDescription, string? employeeFirstname, string? employeeLastname, string? employeeMiddlename)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByCompositeSearch(login, roleName, roleDescription, employeeFirstname, employeeLastname, employeeMiddlename));
        }
    }
}
