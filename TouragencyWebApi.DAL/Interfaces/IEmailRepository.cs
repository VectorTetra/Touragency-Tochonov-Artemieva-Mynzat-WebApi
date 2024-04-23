using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetAll();
        Task<IEnumerable<Email>> Get200Last();
        Task<IEnumerable<Email>> GetByClientId(int clientId);
        Task<IEnumerable<Email>> GetByPersonId(int personId);
        Task<IEnumerable<Email>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<Email>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<Email>> GetByContactTypeId(int contactTypeId);
        Task<IEnumerable<Email>> GetByEmailAddress(string emailAddressSubstring);
        Task<Email?> GetById(long id);
        Task Create(Email email);
        void Update(Email email);
        Task Delete(long id);
        Task<IEnumerable<Email>> GetByFirstname(string firstname);
        Task<IEnumerable<Email>> GetByLastname(string lastname);
        Task<IEnumerable<Email>> GetByMiddlename(string middlename);
        Task<IEnumerable<Email>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
            string? touristNickname, int? contactTypeId, string? emailAddressSubstring, string? firstname,
            string? lastname, string? middlename);
    }
}
