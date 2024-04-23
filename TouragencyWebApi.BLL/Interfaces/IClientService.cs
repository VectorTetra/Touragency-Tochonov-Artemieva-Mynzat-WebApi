using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IClientService
    {
        Task<ClientDTO> TryToRegister(ClientRegisterDTO regDto);
        Task<ClientDTO> TryToLogin(ClientLoginDTO loginDto);
        Task<IEnumerable<ClientDTO>> GetAll();
        Task<IEnumerable<ClientDTO>> Get200Last();
        Task<ClientDTO?> GetById(int clientId);
        Task<ClientDTO?> GetByPersonId(int personId);
        Task<ClientDTO?> GetByBookingId(long bookingId);
        Task<IEnumerable<ClientDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<ClientDTO>> GetByFirstname(string firstname);
        Task<IEnumerable<ClientDTO>> GetByLastname(string lastname);
        Task<IEnumerable<ClientDTO>> GetByMiddlename(string middlename);
        Task<IEnumerable<ClientDTO>> GetByPhoneNumber(string phoneNumber);
        Task<IEnumerable<ClientDTO>> GetByEmailAddress(string emailAddress);
        Task<IEnumerable<ClientDTO>> GetByCompositeSearch(string? touristNickname, string? emailAddress,
           string? phoneNumber, string? firstname, string? lastname, string? middlename);
        Task<ClientDTO> Update(ClientDTO clientDTO);
        Task<ClientDTO> Delete(int id);
    }
}
