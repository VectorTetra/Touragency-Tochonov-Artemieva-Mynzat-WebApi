using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IPhoneRepository
    {
        Task<IEnumerable<Phone>> GetAll();
        Task<IEnumerable<Phone>> GetByClientId(int clientId);
        Task<IEnumerable<Phone>> GetByPersonId(int personId);
        Task<IEnumerable<Phone>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<Phone>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<Phone>> GetByContactTypeId(int contactTypeId);
        Task<IEnumerable<Phone>> GetByPhoneNumber(string phoneNumberSubstring);
        Task<Phone?> GetById(long id);
        Task Create(Phone phone);
        void Update(Phone phone);
        Task Delete(long id);
    }
}
