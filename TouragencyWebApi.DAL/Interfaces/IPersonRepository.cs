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
        Task<IEnumerable<Person>> Get200Last();
        Task<IEnumerable<Person>> GetByIds(ICollection<int> ids);
        Task<IEnumerable<Person>> GetByPhoneNumberSubstring(string phoneNumberSubstring);
        Task<IEnumerable<Person>> GetByEmailAddressSubstring(string emailAddressSubstring);
        Task<IEnumerable<Person>> GetByFirstnameSubstring(string firstnameSubstring);
        Task<IEnumerable<Person>> GetByLastnameSubstring(string lastnameSubstring);
        Task<IEnumerable<Person>> GetByMiddlenameSubstring(string middlenameSubstring);
        Task<IEnumerable<Person>> GetByCompositeSearch(ICollection<int>? ids, string? firstnameSubstring,
            string? lastnameSubstring, string? middlenameSubstring, string? phoneNumberSubstring, string? emailAddressSubstring);
        Task<Person?> GetById(int id);
        Task<Person?> GetByClientId(int clientId);
        Task<Person?> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task Create(Person person);
        void Update(Person person);
        Task Delete(int id);
    }
}
