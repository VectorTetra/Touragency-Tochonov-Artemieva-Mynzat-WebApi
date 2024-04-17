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
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByName(string name);
        Task<IEnumerable<TouragencyEmployeeDTO>> GetByPosition(string position);
        Task Add(TouragencyEmployeeDTO employee);
        Task Update(TouragencyEmployeeDTO employee);
        Task Delete(int id);
    }
}
