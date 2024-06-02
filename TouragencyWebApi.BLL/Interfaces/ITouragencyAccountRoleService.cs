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
    public interface ITouragencyAccountRoleService
    {
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetAll();
        Task<TouragencyAccountRoleDTO?> GetById(int id);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByName(string name);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByDescription(string description);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeFirstname(string employeeFirstname);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeLastname(string employeeLastname);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeMiddlename(string employeeMiddlename);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByCompositeSearch(string? name, string? description, string? employeeFirstname,
            string? employeeLastname, string? employeeMiddlename, string? clientFirstname, string? clientLastname, string? clientMiddlename);
        Task<TouragencyAccountRoleDTO> Add(TouragencyAccountRoleDTO entity);
        Task<TouragencyAccountRoleDTO> Update(TouragencyAccountRoleDTO entity);
        Task<TouragencyAccountRoleDTO> Delete(int id);
    }
}
