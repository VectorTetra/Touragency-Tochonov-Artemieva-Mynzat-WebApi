using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITourStateRepository
    {
        Task<IEnumerable<TourState>> GetAll();
        Task<TourState?> GetById(int id);
        Task<IEnumerable<TourState>> GetByStatus(string status);
        //Task<IEnumerable<TourState>> GetByTourId(int id);
        //Task<IEnumerable<TourState>> GetByTourName(string tourName);
        Task Create(TourState tourState);
        void Update(TourState tourState);
        Task Delete(int id);
    }
}
