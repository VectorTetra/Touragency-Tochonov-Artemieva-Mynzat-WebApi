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
    public interface IEmailService
    {
        Task<EmailDTO> TryToAddNewEmail(EmailDTO emailDTO);
        Task<EmailDTO> Update(EmailDTO emailDTO);
        Task<EmailDTO> Delete(long id);
        Task<IEnumerable<EmailDTO>> GetAll();
        Task<IEnumerable<EmailDTO>> Get200Last();
        Task<EmailDTO?> GetById(long id);
        Task<IEnumerable<EmailDTO>> GetByClientId(int clientId);
        Task<IEnumerable<EmailDTO>> GetByPersonId(int personId);
        Task<IEnumerable<EmailDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<EmailDTO>> GetByContactTypeId(int contactTypeId);
        Task<IEnumerable<EmailDTO>> GetByEmailAddress(string emailAddressSubstring);
        Task<IEnumerable<EmailDTO>> GetByFirstname(string firstname);
        Task<IEnumerable<EmailDTO>> GetByLastname(string lastname);
        Task<IEnumerable<EmailDTO>> GetByMiddlename(string middlename);
        Task<IEnumerable<EmailDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<EmailDTO>> GetByCompositeSearch(int? clientId, int? personId, int? touragencyEmployeeId,
            string? touristNickname, int? contactTypeId, string? emailAddressSubstring, string? firstname,
            string? lastname, string? middlename);
    }
}

