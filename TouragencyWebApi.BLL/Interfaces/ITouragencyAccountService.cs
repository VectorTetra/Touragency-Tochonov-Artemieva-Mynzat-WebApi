using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITouragencyAccountService
    {
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetAll();
        Task<TouragencyEmployeeAccountDTO> GetById(int id);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByLogin(string login);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRole(string role);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeName(string employeeName);
        Task Add(TouragencyEmployeeAccountDTO accountDTO);
        Task Update(TouragencyEmployeeAccountDTO accountDTO);
        Task Delete(int id);
    }
}
