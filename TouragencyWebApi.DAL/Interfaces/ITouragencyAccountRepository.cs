using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITouragencyAccountRepository
    {
        Task<IEnumerable<TouragencyEmployeeAccount>> GetAll();
        Task<IEnumerable<TouragencyEmployeeAccount>> Get200Last();
        Task<TouragencyEmployeeAccount> GetById(int id);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByLogin(string login);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByRoleName(string roleName);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByRoleDescription(string roleDescription);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeFirstname(string employeeFirstname);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeLastname(string employeeLastname);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeMiddlename(string employeeMiddlename);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByCompositeSearch(string? login, string? roleName,
            string? roleDescription, string? employeeFirstname, string? employeeLastname, string? employeeMiddlename);
        Task Create(TouragencyEmployeeAccount account);
        void Update(TouragencyEmployeeAccount account);
        Task Delete(int id);
    }
}
