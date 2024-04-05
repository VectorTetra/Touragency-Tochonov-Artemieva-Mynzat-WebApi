using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TourStateService : ITourStateService
    {
        IUnitOfWork Database;

        MapperConfiguration State_StateDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TourState, TourStateDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Status", opt => opt.MapFrom(c => c.Status))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        );

        public TourStateService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Add(TourStateDTO tourStateDTO)
        {
            var PreExistedState = await Database.TourStates.GetByStatus(tourStateDTO.Status);
            if (PreExistedState.Any(em => em.Status == tourStateDTO.Status))
            {
                throw new ValidationException("Такий TourState вже існує", "");
            }
            var newState = new TourState
            {
                Status = tourStateDTO.Status,
                Description = tourStateDTO.Description
            };

            //foreach (var id in tourStateDTO.TourIds)
            //{
            //    var tour = await Database.Tours.GetById(id);
            //    if (tour != null)
            //    {
            //        newState.Tours.Add(tour);
            //    }
            //}
            await Database.TourStates.Create(newState);
            await Database.Save();
        }
        public async Task Update(TourStateDTO tourStateDTO)
        {
            TourState state = await Database.TourStates.GetById(tourStateDTO.Id);

            if (state == null)
                throw new ValidationException("Такий TourState не знайдено", "");

            state.Status = tourStateDTO.Status;
            state.Description = tourStateDTO.Description;
            //state.Tours.Clear();
            //foreach (var id in tourStateDTO.TourIds)
            //{
            //    var tour = await Database.Tours.GetById(id);
            //    if (tour != null)
            //    {
            //        state.Tours.Add(tour);
            //    }
            //}
            Database.TourStates.Update(state);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            TourState state = await Database.TourStates.GetById(id);

            if (state == null)
                throw new ValidationException("Такий TourState не знайдено", "");

            await Database.TourStates.Delete(id);
            await Database.Save();
        }
        public async Task<IEnumerable<TourStateDTO>> GetAll()
        {
            var mapper = new Mapper(State_StateDTOMapConfig);
            return mapper.Map<IEnumerable<TourState>, IEnumerable<TourStateDTO>>(await Database.TourStates.GetAll());
        }
        public async Task<TourStateDTO?> GetById(int id)
        {
            var mapper = new Mapper(State_StateDTOMapConfig);
            return mapper.Map<TourState, TourStateDTO>(await Database.TourStates.GetById(id));
        }
        public async Task<IEnumerable<TourStateDTO>> GetByStatus(string status)
        {
            var mapper = new Mapper(State_StateDTOMapConfig);
            return mapper.Map<IEnumerable<TourState>, IEnumerable<TourStateDTO>>(await Database.TourStates.GetByStatus(status));
        }
        //Task<IEnumerable<TourStateDTO>> GetByTourId(int id)
        //{
        //}
        //Task<IEnumerable<TourStateDTO>> GetByTourName(string tourName);
        //{
        //}
    }
}
