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
    public interface ITouragencyAccountService
    {
        Task<TouragencyEmployeeAccountDTO> TryToRegister(TouragencyAccountRegisterDTO reg);
        Task<TouragencyEmployeeAccountDTO> TryToLogin(TouragencyAccountLoginDTO login);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetAll();
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> Get200Last();
        Task<TouragencyEmployeeAccountDTO> GetById(int id);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByLogin(string login);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRoleName(string roleName);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRoleDescription(string roleDescription);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeFirstname(string employeeFirstname);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeLastname(string employeeLastname);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeMiddlename(string employeeMiddlename);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByCompositeSearch(string? login, string? roleName,
            string? roleDescription, string? employeeFirstname, string? employeeLastname, string? employeeMiddlename);
        Task<TouragencyEmployeeAccountDTO> Update(TouragencyEmployeeAccountDTO accountDTO);
        Task<TouragencyEmployeeAccountDTO> Delete(int id);
    }
}
