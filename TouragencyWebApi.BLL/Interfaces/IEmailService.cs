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
        Task TryToAddNewEmail(EmailDTO emailDTO);
        Task Update(EmailDTO emailDTO);
        Task Delete(long id);
        Task<IEnumerable<EmailDTO>> GetAll();
        Task<EmailDTO?> GetById(long id);
        Task<IEnumerable<EmailDTO>> GetByClientId(int clientId);
        Task<IEnumerable<EmailDTO>> GetByPersonId(int personId);
        Task<IEnumerable<EmailDTO>> GetByTouragencyEmployeeId(int touragencyEmployeeId);
        Task<IEnumerable<EmailDTO>> GetByContactTypeId(int contactTypeId);
        Task<EmailDTO?> GetByEmailAddress(string emailAddress);
    }
}

