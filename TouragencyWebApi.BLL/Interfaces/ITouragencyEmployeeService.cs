using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITouragencyEmployeeService
    {
        Task<IEnumerable<TouragencyEmployeeDTO>> GetAll();
        Task<TouragencyEmployeeDTO?> GetById(int id);

        Task<TouragencyEmployeeDTO?> GetByAccountId(int accountId);
        Task<TouragencyEmployeeDTO> Add(TouragencyEmployeeDTO employee);
        Task<TouragencyEmployeeDTO> Update(TouragencyEmployeeDTO employee);
        Task<TouragencyEmployeeDTO> Delete(int id);

        Task<IEnumerable<TouragencyEmployeeDTO>> Get200Last();
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByFirstname(string firstname);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByLastname(string lastname);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByMiddlename(string middlename);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByPositionName(string positionName);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByPositionDescription(string positionDescription);

        Task<IEnumerable<TouragencyEmployeeDTO>> GetByAccountLogin(string touragencyAccountLogin);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByAccountRoleId(int touragencyAccountRoleId);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByCompositeSearch(string? firstname, string? lastname,
            string? middlename, string? positionName, string? positionDescription, string? touragencyAccountLogin, int? touragencyAccountRoleId);
    }
}
