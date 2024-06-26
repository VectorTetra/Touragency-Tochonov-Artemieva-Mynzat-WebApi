﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using AutoMapper;
using TouragencyWebApi.DAL.Entities;
namespace TouragencyWebApi.BLL.Services
{
    public class PositionService : IPositionService
    {
        IUnitOfWork Database;
        public PositionService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration Position_PositionDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForPath(d => d.TouragencyEmployeeIds, opt => opt.MapFrom(c => c.TouragencyEmployees.Select(te => te.Id)))
        );
        public async Task<PositionDTO> Create(PositionDTO positionDTO)
        {
            var BusyPositionId = await Database.Positions.GetById(positionDTO.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyPositionId != null)
            {
                throw new ValidationException("Такий positionId вже зайнято!", nameof(positionDTO.Id));
            }
            var newPosition = new Position
            {
                Name = positionDTO.Name,
                Description = positionDTO.Description,
                TouragencyEmployees = new List<TouragencyEmployee>()
            };

            foreach (var id in positionDTO.TouragencyEmployeeIds)
            {
                var touragencyEmployee = await Database.TouragencyEmployees.GetById(id);
                if (touragencyEmployee != null)
                {
                    newPosition.TouragencyEmployees.Add(touragencyEmployee);
                }
            }
            await Database.Positions.Create(newPosition);
            await Database.Save();
            positionDTO.Id = newPosition.Id;
            return positionDTO;
        }
        public async Task<PositionDTO> Update(PositionDTO positionDTO)
        {
            var BusyPosition = await Database.Positions.GetById(positionDTO.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyPosition != null)
            {
                throw new ValidationException("Такий positionId вже зайнято!", nameof(positionDTO.Id));
            }
            BusyPosition.Description = positionDTO.Description;
            BusyPosition.Name = positionDTO.Name;
            BusyPosition.TouragencyEmployees.Clear();
            foreach (var id in positionDTO.TouragencyEmployeeIds)
            {
                var touragencyEmployee = await Database.TouragencyEmployees.GetById(id);
                if (touragencyEmployee == null)
                {
                    throw new ValidationException("Такий touragencyEmployeeId не знайдено", nameof(positionDTO.TouragencyEmployeeIds));
                }
                    BusyPosition.TouragencyEmployees.Add(touragencyEmployee);
            }
            Database.Positions.Update(BusyPosition);
            await Database.Save();
            return positionDTO;
        }
        public async Task<PositionDTO> Delete(int id)
        {
            Position position = await Database.Positions.GetById(id);
            if (position == null)
            {
                throw new ValidationException("Посаду не знайдено", "");
            }
            var dto = await GetById(id);
            await Database.Positions.Delete(id);
            await Database.Save();
            return dto;
        }
        public async Task<IEnumerable<PositionDTO>> GetAll()
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetAll());
        }

        public async Task<IEnumerable<PositionDTO>> Get200Last()
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.Get200Last());
        }
        public async Task<IEnumerable<PositionDTO>> GetByDescriptionSubstring(string positionDescriptionSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByDescriptionSubstring(positionDescriptionSubstring));
        }

        public async Task<IEnumerable<PositionDTO>> GetByNameSubstring(string positionNameSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByNameSubstring(positionNameSubstring));
        }

        public async Task<PositionDTO?> GetByPersonId(int id)
        {
            var position = await Database.Positions.GetByPersonId(id);
            if (position == null)
            {
                return null;
            }
            var positionDTO = new PositionDTO
            {
                Id = position.Id,
                Description = position.Description,
                TouragencyEmployeeIds = position.TouragencyEmployees.Select(te => te.Id).ToList()
            };
            return positionDTO;
        }
        public async Task<PositionDTO?> GetByTouragencyEmployeeId(int id)
        {
            var position = await Database.Positions.GetByTouragencyEmployeeId(id);
            if (position == null)
            {
                return null;
            }
            var positionDTO = new PositionDTO
            {
                Id = position.Id,
                Description = position.Description,
                TouragencyEmployeeIds = position.TouragencyEmployees.Select(te => te.Id).ToList()
            };
            return positionDTO;
        }
        public async Task<PositionDTO?> GetById(int id)
        {
            var position = await Database.Positions.GetById(id);
            if (position == null)
            {
                return null;
            }
            var positionDTO = new PositionDTO
            {
                Id = position.Id,
                Description = position.Description,
                TouragencyEmployeeIds = position.TouragencyEmployees.Select(te => te.Id).ToList()
            };
            return positionDTO;
        }

        public async Task<IEnumerable<PositionDTO>> GetByPersonFirstnameSubstring(string personFirstnameSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByPersonFirstnameSubstring(personFirstnameSubstring));
        }

        public async Task<IEnumerable<PositionDTO>> GetByPersonLastnameSubstring(string personLastnameSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByPersonLastnameSubstring(personLastnameSubstring));
        }

        public async Task<IEnumerable<PositionDTO>> GetByPersonMiddlenameSubstring(string personMiddlenameSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByPersonMiddlenameSubstring(personMiddlenameSubstring));
        }

        public async Task<IEnumerable<PositionDTO>> GetByCompositeSearch(string? positionNameSubstring, string? positionDescriptionSubstring,
                       string? personFirstnameSubstring, string? personLastnameSubstring, string? personMiddlenameSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByCompositeSearch(positionNameSubstring, positionDescriptionSubstring, personFirstnameSubstring, personLastnameSubstring, personMiddlenameSubstring));
        }
    }
}
