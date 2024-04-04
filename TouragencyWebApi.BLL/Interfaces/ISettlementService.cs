using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ISettlementService
    {
        Task<IEnumerable<SettlementDTO>> GetAll();
        Task<SettlementDTO?> GetById(int id);
        Task<IEnumerable<SettlementDTO>> GetByName(string name);
        Task<IEnumerable<SettlementDTO>> GetByCountryName(string countryName);
        Task Add(SettlementDTO settlementDTO);
        Task Update(SettlementDTO settlementDTO);
        Task Delete(int id);

    }
}
