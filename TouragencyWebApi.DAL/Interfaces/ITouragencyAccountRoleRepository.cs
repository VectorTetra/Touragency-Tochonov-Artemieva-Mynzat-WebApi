using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITouragencyAccountRoleRepository
    {
        Task<IEnumerable<TouragencyAccountRole>> GetAll();
        Task<TouragencyAccountRole?> GetById(int id);
        Task<IEnumerable<TouragencyAccountRole>> GetByName(string name);
        Task<IEnumerable<TouragencyAccountRole>> GetByDescription(string description);
        Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeFirstname(string employeeFirstname);
        Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeLastname(string employeeLastname);
        Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeMiddlename(string employeeMiddlename);
        Task<IEnumerable<TouragencyAccountRole>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<TouragencyAccountRole>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<TouragencyAccountRole>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<TouragencyAccountRole>> GetByCompositeSearch(string? name, string? description, string? employeeFirstname,
            string? employeeLastname, string? employeeMiddlename, string? clientFirstname, string? clientLastname, string? clientMiddlename);
        Task Create(TouragencyAccountRole entity);
        void Update(TouragencyAccountRole entity);
        Task Delete(int id);   
    }
}
