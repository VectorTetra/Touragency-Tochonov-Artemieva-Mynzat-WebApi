using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client?> GetByClientId(int clientId);
        Task<Client?> GetByPersonId(int personId);
        Task<Client?> GetByBookingId(int bookingId);
        Task<IEnumerable<Client>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<Client>> GetByFirstname(string firstname);
        Task<IEnumerable<Client>> GetByLastname(string lastname);
        Task<IEnumerable<Client>> GetByMiddlename(string middlename);
        Task Create(Client client);
        void Update(Client client);
        Task Delete(int id);
    }
}
