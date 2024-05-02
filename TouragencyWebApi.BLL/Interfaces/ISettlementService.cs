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
        Task<IEnumerable<SettlementDTO>> Get200Last();
        Task<SettlementDTO?> GetById(int id);
        Task<IEnumerable<SettlementDTO>> GetByName(string name);
        Task<IEnumerable<SettlementDTO>> GetByCountryName(string countryName);
        Task<IEnumerable<SettlementDTO>> GetByCountryId(int countryId);
        Task<IEnumerable<SettlementDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<SettlementDTO>> GetByTourName(string tourName);
        Task<IEnumerable<SettlementDTO>> GetByCompositeSearch(string? name, string? countryName, int? countryId, int? tourNameId, string? tourName);
        Task<SettlementDTO?> GetByHotelId(int hotelId);
        Task<SettlementDTO> Add(SettlementDTO settlementDTO);
        Task<SettlementDTO> Update(SettlementDTO settlementDTO);
        Task<SettlementDTO> Delete(int id);

    }
}
