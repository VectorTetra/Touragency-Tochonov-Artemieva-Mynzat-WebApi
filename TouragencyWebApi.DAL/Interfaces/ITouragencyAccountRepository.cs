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
        Task<TouragencyEmployeeAccount> GetById(int id);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByLogin(string login);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByRole(string role);
        Task<IEnumerable<TouragencyEmployeeAccount>> GetByEmployeeName(string employeeName);
        Task Create(TouragencyEmployeeAccount account);
        void Update(TouragencyEmployeeAccount account);
        Task Delete(int id);
    }
}
