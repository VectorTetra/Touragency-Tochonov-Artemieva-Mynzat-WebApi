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
        Task TryToRegister(TouragencyAccountRegisterDTO reg);
        Task<TouragencyEmployeeAccountDTO> TryToLogin(TouragencyAccountLoginDTO login);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetAll();
        Task<TouragencyEmployeeAccountDTO> GetById(int id);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByLogin(string login);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRole(string role);
        Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeName(string employeeName);
        Task Update(TouragencyEmployeeAccountDTO accountDTO);
        Task Delete(int id);
    }
}
