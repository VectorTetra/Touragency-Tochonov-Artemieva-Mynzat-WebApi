using System;
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
    public class PositionService: IPositionService
    {
        IUnitOfWork Database;
        public PositionService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration Position_PositionDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Position, PositionDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.TouragencyEmployeeIds, opt => opt.MapFrom(c => c.TouragencyEmployees.Select(te => te.Id)))
        );
        public async Task Create(PositionDTO positionDTO)
        {
            var BusyPositionId = await Database.Positions.GetById(positionDTO.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyPositionId != null)
            {
                throw new ValidationException("Такий positionId вже зайнято!", nameof(positionDTO.Id));
            }
            var newPosition = new Position
            {
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
        }
        public async Task Update(PositionDTO positionDTO)
        {
            var BusyPositionId = await Database.Positions.GetById(positionDTO.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyPositionId != null)
            {
                throw new ValidationException("Такий positionId вже зайнято!", nameof(positionDTO.Id));
            }
            var newPosition = new Position
            {
                Id = positionDTO.Id,
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
            Database.Positions.Update(newPosition);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            Position position = await Database.Positions.GetById(id);
            if (position == null)
            {
                throw new ValidationException("Посаду не знайдено", "");
            }
            await Database.Positions.Delete(id);
            await Database.Save();
        }
        public async Task<IEnumerable<PositionDTO>> GetAll()
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetAll());
        }
        public async Task<IEnumerable<PositionDTO>> GetByDescriptionSubstring(string positionDescriptionSubstring)
        {
            var mapper = new Mapper(Position_PositionDTOMapConfig);
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(await Database.Positions.GetByDescriptionSubstring(positionDescriptionSubstring));
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
    }
}
