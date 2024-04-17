using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITouragencyEmployeeRepository
    {
        Task<IEnumerable<TouragencyEmployee>> GetAll();
        Task<TouragencyEmployee?> GetById(int id);
        Task<IEnumerable<TouragencyEmployee>> GetByName(string name);
        Task<IEnumerable<TouragencyEmployee>> GetByPosition(string position);
        Task Create(TouragencyEmployee employee);
        void Update(TouragencyEmployee employee);
        Task Delete(int id);
    }
}
