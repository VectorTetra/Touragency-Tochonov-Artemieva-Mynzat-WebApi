using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITouragencyAccountRoleService
    {
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetAll();
        Task<TouragencyAccountRoleDTO?> GetById(int id);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByName(string name);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeName(string employeeName);
        Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientName(string clientName);
        Task Add(TouragencyAccountRoleDTO entity);
        Task Update(TouragencyAccountRoleDTO entity);
        Task Delete(int id);
    }
}
