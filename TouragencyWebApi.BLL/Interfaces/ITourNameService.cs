using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourNameService
    {
        Task<IEnumerable<TourNameDTO>> GetAll();
        Task<TourNameDTO?> GetById(int id);
        Task<IEnumerable<TourNameDTO>> GetByName(string tourNameSubstring);
        Task Create(TourNameDTO tourNameDTO);
        Task Update(TourNameDTO tourNameDTO);
        Task Delete(int id);
    }
}
