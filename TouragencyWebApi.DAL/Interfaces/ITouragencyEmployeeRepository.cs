using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITouragencyEmployeeRepository
    {
        Task<IEnumerable<TouragencyEmployee>> GetAll();
        Task<IEnumerable<TouragencyEmployee>> Get200Last();
        Task<TouragencyEmployee?> GetById(int id);
        Task<TouragencyEmployee?> GetByAccountId(int accountId);
        Task<IEnumerable<TouragencyEmployee>> GetByFirstname(string firstname);
        Task<IEnumerable<TouragencyEmployee>> GetByLastname(string lastname);
        Task<IEnumerable<TouragencyEmployee>> GetByMiddlename(string middlename);
        Task<IEnumerable<TouragencyEmployee>> GetByPositionName(string positionName);
        Task<IEnumerable<TouragencyEmployee>> GetByPositionDescription(string positionDescription);
        Task<IEnumerable<TouragencyEmployee>> GetByEmailAddress(string emailAddress);
        Task<IEnumerable<TouragencyEmployee>> GetByPhoneNumber(string phoneNumber);
        Task<IEnumerable<TouragencyEmployee>> GetByAccountLogin(string touragencyAccountLogin);
        Task<IEnumerable<TouragencyEmployee>> GetByAccountRoleId(int touragencyAccountRoleId);
        Task<IEnumerable<TouragencyEmployee>> GetByCompositeSearch(string? firstname, string? lastname,
            string? middlename, string? positionName, string? positionDescription, string? touragencyAccountLogin, int? touragencyAccountRoleId, 
            string? emailAddress, string? phoneNumber);
        Task Create(TouragencyEmployee employee);
        void Update(TouragencyEmployee employee);
        Task Delete(int id);
    }
}
