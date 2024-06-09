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
        Task<IEnumerable<Client>> Get200Last();
        Task<Client?> GetById(int clientId);
        Task<Client?> GetByPersonId(int personId);
        Task<Client?> GetByBookingId(long bookingId);
        Task<IEnumerable<Client>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<Client>> GetByEmailAddress(string emailAddress);
        Task<IEnumerable<Client>> GetByPhoneNumber(string phoneNumber);
        Task<IEnumerable<Client>> GetByFirstname(string firstname);
        Task<IEnumerable<Client>> GetByLastname(string lastname);
        Task<IEnumerable<Client>> GetByMiddlename(string middlename);
        Task<IEnumerable<Phone>> GetPhones(int clientId);
        Task<IEnumerable<Email>> GetEmails(int clientId);
        Task<IEnumerable<Booking>> GetBookings(int clientId);
        Task<IEnumerable<Client>> GetByCompositeSearch(string? touristNickname, string? emailAddress,
            string? phoneNumber, string? firstname, string? lastname, string? middlename);
        Task Create(Client client);
        void Update(Client client);
        Task Delete(int id);
    }
}
