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
        Task<IEnumerable<TouragencyAccountRole>> GetByEmployeeName(string employeeName);
        Task<IEnumerable<TouragencyAccountRole>> GetByClientName(string  clientName);
        Task Create(TouragencyAccountRole entity);
        void Update(TouragencyAccountRole entity);
        Task Delete(int id);   
    }
}
