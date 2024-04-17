using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TouragencyEmployeeService : ITouragencyEmployeeService
    {
        IUnitOfWork Database;

        MapperConfiguration Employee_EmployeeDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TouragencyEmployee, TouragencyEmployeeDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("PositionId", opt => opt.MapFrom(c => c.PositionId))
        .ForMember("PersonId", opt => opt.MapFrom(c => c.PersonId))
        );

        public TouragencyEmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Add(TouragencyEmployeeDTO employeeDTO)
        {
            var person = await Database.Persons.GetById(employeeDTO.PersonId);
            var PreExistedEmployee = await Database.TouragencyEmployees.GetByName(person.Lastname);
            if (PreExistedEmployee.Any(em => em.Person.Lastname == person.Lastname))
            {
                throw new ValidationException("Така працівник вже існує", "");
            }
            var newEmployee = new TouragencyEmployee
            {
                PositionId = employeeDTO.PositionId,
                PersonId = employeeDTO.PersonId
            };

            await Database.TouragencyEmployees.Create(newEmployee);
            await Database.Save();
        }

        public async Task Update(TouragencyEmployeeDTO employeeDTO)
        {
            TouragencyEmployee employee = await Database.TouragencyEmployees.GetById(employeeDTO.Id);
            if (employee == null)
            {
                throw new ValidationException("Така працівник вже існує", "");
            }
            employee.PositionId = employeeDTO.PositionId;
            employee.PersonId = employeeDTO.PersonId;

            Database.TouragencyEmployees.Update(employee);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            TouragencyEmployee employee = await Database.TouragencyEmployees.GetById(id);
            if (employee == null)
            {
                throw new ValidationException("Така працівник вже існує", "");
            }
            await Database.TouragencyEmployees.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetAll()
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetAll());
        }

        public async Task<TouragencyEmployeeDTO?> GetById(int id)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<TouragencyEmployee, TouragencyEmployeeDTO>(await Database.TouragencyEmployees.GetById(id));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByName(string employeeName)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByName(employeeName));
        }
        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByPosition(string position)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByPosition(position));
        }

    }
}
