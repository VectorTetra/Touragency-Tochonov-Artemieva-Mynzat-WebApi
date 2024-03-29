using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IPhoneService
    {
        Task TryToAddNewPhone(PhoneDTO phoneDTO);
        Task Update(PhoneDTO phoneDTO);
        Task Delete(long id);
        Task<IEnumerable<PhoneDTO>> GetAll();
        Task<PhoneDTO?> GetById(long id);
        Task<IEnumerable<PhoneDTO>> GetByClientId(int clientId);
        Task<IEnumerable<PhoneDTO>> GetByPersonId(int personId);
        Task<IEnumerable<PhoneDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<PhoneDTO>> GetByContactTypeId(int contactTypeId);
        Task<IEnumerable<PhoneDTO>> GetByPhoneNumber(string phoneNumberSubstring);
    }
}
