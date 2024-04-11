﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DAL.Interfaces;
using AutoMapper;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Services
{
    public class TourNameService : ITourNameService
    {
        IUnitOfWork Database;
        MapperConfiguration TourName_TourNameDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TourName, TourNameDTO>()
       .ForMember("Id", opt => opt.MapFrom(c => c.Id))
       .ForMember("Name", opt => opt.MapFrom(c => c.Name))
       .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        );
        public TourNameService(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public async Task<IEnumerable<TourNameDTO>> GetAll()
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetAll());
        }
        public async Task<TourNameDTO?> GetById(int id)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<TourName, TourNameDTO>(await Database.TourNames.GetById(id));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByName(string tourNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByName(tourNameSubstring));
        }
        public async Task Create(TourNameDTO tourNameDTO)
        {
            var Existed = await Database.TourNames.GetByName(tourNameDTO.Name);
            if (Existed.Any(em => em.Name == tourNameDTO.Name))
            {
                throw new ValidationException("Така назва туру вже існує", "");
            }
            var newTourName = new TourName
            {
                Name = tourNameDTO.Name,
                Tours = new List<Tour>()
            };
            foreach (var id in tourNameDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour != null)
                {
                    newTourName.Tours.Add(tour);
                }
            }
            await Database.TourNames.Create(newTourName);
            await Database.Save();
        }
        public async Task Update(TourNameDTO tourNameDTO)
        {
            TourName tourName = await Database.TourNames.GetById(tourNameDTO.Id);
            if (tourName == null)
            {
                throw new ValidationException("Така назва туру не знайдена", "");
            }
            tourName.Name = tourNameDTO.Name;
            tourName.Tours.Clear();
            foreach (var id in tourNameDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour != null)
                {
                    tourName.Tours.Add(tour);
                }
            }
            Database.TourNames.Update(tourName);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            await Database.TourNames.Delete(id);
            await Database.Save();
        }
       
    }
}