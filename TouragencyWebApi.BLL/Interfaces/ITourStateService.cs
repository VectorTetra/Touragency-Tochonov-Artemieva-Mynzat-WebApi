using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourStateService
    {
        Task<IEnumerable<TourStateDTO>> GetAll();
        Task<TourStateDTO?> GetById(int id);
        Task<IEnumerable<TourStateDTO>> GetByStatus(string status);
        //Task<IEnumerable<TourStateDTO>> GetByTourId(int id);
        //Task<IEnumerable<TourStateDTO>> GetByTourName(string tourName);
        Task Add(TourStateDTO tourStateDTO);
        Task Update(TourStateDTO tourStateDTO);
        Task Delete(int id);
    }
}
