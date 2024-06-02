using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IPhoneService
    {
        Task<PhoneDTO> TryToAddNewPhone(PhoneDTO phoneDTO);
        Task<PhoneDTO> Update(PhoneDTO phoneDTO);
        Task<PhoneDTO> Delete(long id);
        Task<IEnumerable<PhoneDTO>> GetAll();
        Task<IEnumerable<PhoneDTO>> Get200Last();
        Task<PhoneDTO?> GetById(long id);
        Task<IEnumerable<PhoneDTO>> GetByClientId(int clientId);
        Task<IEnumerable<PhoneDTO>> GetByPersonId(int personId);
        Task<IEnumerable<PhoneDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<PhoneDTO>> GetByContactTypeId(int contactTypeId);
        Task<IEnumerable<PhoneDTO>> GetByPhoneNumber(string phoneNumberSubstring);
        Task<IEnumerable<PhoneDTO>> GetByFirstname(string firstname);
        Task<IEnumerable<PhoneDTO>> GetByLastname(string lastname);
        Task<IEnumerable<PhoneDTO>> GetByMiddlename(string middlename);
        Task<IEnumerable<PhoneDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<PhoneDTO>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
            string? touristNickname, int? contactTypeId, string? phoneNumberSubstring, string? firstname,
            string? lastname, string? middlename);
    }
}
