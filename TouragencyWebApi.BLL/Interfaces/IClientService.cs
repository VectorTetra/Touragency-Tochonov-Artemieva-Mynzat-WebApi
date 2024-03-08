using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IClientService
    {
        Task TryToRegister(ClientRegisterDTO regDto);
        Task<ClientDTO> TryToLogin(ClientLoginDTO loginDto);
        Task<IEnumerable<ClientDTO>> GetAll();
        Task<ClientDTO?> GetByClientId(int clientId);
        Task<ClientDTO?> GetByPersonId(int personId);
        Task<ClientDTO?> GetByBookingId(int bookingId);
        Task<IEnumerable<ClientDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<ClientDTO>> GetByFirstname(string firstname);
        Task<IEnumerable<ClientDTO>> GetByLastname(string lastname);
        Task<IEnumerable<ClientDTO>> GetByMiddlename(string middlename);
        Task Create(ClientDTO client);
        void Update(ClientDTO client);
        Task Delete(int id);
    }
}
