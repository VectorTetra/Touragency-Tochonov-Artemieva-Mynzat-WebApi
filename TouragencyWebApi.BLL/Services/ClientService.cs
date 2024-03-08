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
namespace TouragencyWebApi.Services
{
    public class ClientService: IClientService
    {
        IUnitOfWork Database;
        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task TryToRegister(ClientRegisterDTO regDto)
        {
            //Перш ніж зареєструватись, треба перевірити, чи є такий нік туриста в БД
            var BusyLoginClientsCollection = await Database.Clients.GetByTouristNickname(regDto.TouristNickname);
            if (BusyLoginClientsCollection.ToList().Any(x => x.TouristNickname == regDto.TouristNickname))
            {
                throw new ValidationException("Такий нік туриста вже зайнято!", "");
            }
            // Також треба перевірити, чи паролі співпадають
            if (regDto.Password != regDto.PasswordConfirm)
            {
                throw new ValidationException("Паролі не співпадають!", "");
            }
            var newClient = new Client
            { 
                TouristNickname = regDto.TouristNickname
            };

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

            newUser.Password = hash.ToString();
            newUser.Salt = salt;
            await Database.Users.Create(newUser);
            await Database.Save();
        }
        public async Task<ClientDTO> TryToLogin(ClientLoginDTO loginDto){}
        public async Task<IEnumerable<ClientDTO>> GetAll(){}
        public async Task<ClientDTO?> GetByClientId(int clientId){}
        public async Task<ClientDTO?> GetByPersonId(int personId){}
        public async Task<ClientDTO?> GetByBookingId(int bookingId){}
        public async Task<IEnumerable<ClientDTO>> GetByTouristNickname(string touristNickname){}
        public async Task<IEnumerable<ClientDTO>> GetByFirstname(string firstname){}
        public async Task<IEnumerable<ClientDTO>> GetByLastname(string lastname){}
        public async Task<IEnumerable<ClientDTO>> GetByMiddlename(string middlename){}
        public async Task Create(ClientDTO client){}
        void Update(ClientDTO client){}
        public async Task Delete(int id){}
    }
}
