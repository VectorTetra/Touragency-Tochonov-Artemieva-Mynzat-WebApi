using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAll();
        Task<IEnumerable<PersonDTO>> Get200Last();
        Task<IEnumerable<PersonDTO>> GetByIds(ICollection<int> ids);
        Task<IEnumerable<PersonDTO>> GetByPhoneNumberSubstring(string phoneNumberSubstring);
        Task<IEnumerable<PersonDTO>> GetByEmailAddressSubstring(string emailAddressSubstring);
        Task<IEnumerable<PersonDTO>> GetByFirstnameSubstring(string firstnameSubstring);
        Task<IEnumerable<PersonDTO>> GetByLastnameSubstring(string lastnameSubstring);
        Task<IEnumerable<PersonDTO>> GetByMiddlenameSubstring(string middlenameSubstring);
        Task<IEnumerable<PersonDTO>> GetByCompositeSearch(ICollection<int>? ids, string? firstnameSubstring,
            string? lastnameSubstring, string? middlenameSubstring, string? phoneNumberSubstring, string? emailAddressSubstring);
        Task<PersonDTO> GetById(int id);
        Task<PersonDTO> GetByClientId(int clientId);
        Task<PersonDTO> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task Create(PersonDTO personDTO);
        Task Update(PersonDTO personDTO);
        Task Delete(int id);
    }
}
