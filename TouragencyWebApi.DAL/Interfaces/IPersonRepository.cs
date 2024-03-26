using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<IEnumerable<Person>> GetAllByIds(ICollection<int> ids);
        Task<IEnumerable<Person>> GetAllByPhoneNumber(string phoneNumber);
        Task<IEnumerable<Person>> GetAllByEmailAddress(string emailAddress);
        Task<IEnumerable<Person>> GetAllByFirstname(string firstname);
        Task<IEnumerable<Person>> GetAllByLastname(string lastname);
        Task<IEnumerable<Person>> GetAllByMiddleName(string middlename);
        Task<Person?> GetById(int id);
        Task<Person?> GetByClientId(int clientId);
        Task<Person?> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task Create(Person person);
        void Update(Person person);
        Task Delete(int id);
    }
}
