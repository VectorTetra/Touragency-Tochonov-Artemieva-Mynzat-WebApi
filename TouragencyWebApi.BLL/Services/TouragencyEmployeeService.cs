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
        .ForPath(p => p.AccountId, opt => opt.MapFrom(c => c.Account.Id))
        .ForPath(p => p.Firstname, opt => opt.MapFrom(c => c.Person.Firstname))
        .ForPath(p => p.Lastname, opt => opt.MapFrom(c => c.Person.Lastname))
        .ForPath(p => p.Middlename, opt => opt.MapFrom(c => c.Person.Middlename))
        .ForPath(p => p.Email, opt => opt.MapFrom(c => c.Person.Emails.ElementAt(0).EmailAddress))
        .ForPath(p => p.Phone, opt => opt.MapFrom(c => c.Person.Phones.ElementAt(0).PhoneNumber))
        .ForPath(p => p.PositionName, opt => opt.MapFrom(c => c.Position.Name))
        .ForPath(p => p.PositionDescription, opt => opt.MapFrom(c => c.Position.Description))
        .ForPath(p => p.AccountLogin, opt => opt.MapFrom(c => c.Account.Login))
        );

        public TouragencyEmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<TouragencyEmployeeDTO> Add(TouragencyEmployeeDTO employeeDTO)
        {
            var emp = await Database.TouragencyEmployees.GetById(employeeDTO.PersonId);
            if (emp != null)
            {
                throw new ValidationException($"Такий працівник турагенства вже існує (employeeDTO.PersonId : {employeeDTO.PersonId})", "") ;
            }
            var position = await Database.Positions.GetById(employeeDTO.PositionId);
            if (position == null)
            {
                throw new ValidationException($"Такої посади не існує (employeeDTO.PositionId : {employeeDTO.PositionId})", "");
            }
            var person = await Database.Persons.GetById(employeeDTO.PersonId);
            if (person == null)
            {
                throw new ValidationException($"Такої персони не існує (employeeDTO.PersonId : {employeeDTO.PersonId})", "");
            }
            TouragencyEmployeeAccount? account = null;
            if (employeeDTO.AccountId != null)
            {
                account = await Database.TouragencyAccounts.GetById(employeeDTO.AccountId.Value);
                if (account == null)
                {
                    throw new ValidationException($"Такого акаунта турагента не існує (employeeDTO.AccountId : {employeeDTO.AccountId})", "");
                }

            }
            var newEmployee = new TouragencyEmployee
            {
                PositionId = employeeDTO.PositionId,
                PersonId = employeeDTO.PersonId,
                Account = account
            };

            await Database.TouragencyEmployees.Create(newEmployee);
            await Database.Save();
            employeeDTO.Id = newEmployee.Id;
            return employeeDTO;
        }

        public async Task<TouragencyEmployeeDTO> Update(TouragencyEmployeeDTO employeeDTO)
        {
            TouragencyEmployee employee = await Database.TouragencyEmployees.GetById(employeeDTO.Id);
            if (employee == null)
            {
                throw new ValidationException($"Такого співробітника турагенства не існує (employeeDTO.Id : {employeeDTO.Id})", "");
            }
            var position = await Database.Positions.GetById(employeeDTO.PositionId);
            if (position == null)
            {
                throw new ValidationException($"Такої посади не існує (employeeDTO.PositionId : {employeeDTO.PositionId})", "");
            }
            var person = await Database.Persons.GetById(employeeDTO.PersonId);
            if (person == null)
            {
                throw new ValidationException($"Такої персони не існує (employeeDTO.PersonId : {employeeDTO.PersonId})", "");
            }
            TouragencyEmployeeAccount? account = null;
            if (employeeDTO.AccountId != null)
            {
                account = await Database.TouragencyAccounts.GetById(employeeDTO.AccountId.Value);
                if (account == null)
                {
                    throw new ValidationException($"Такого акаунта турагента не існує (employeeDTO.AccountId : {employeeDTO.AccountId})", "");
                }

            }
            employee.PositionId = employeeDTO.PositionId;
            employee.PersonId = employeeDTO.PersonId;
            employee.Account = account;

            Database.TouragencyEmployees.Update(employee);
            await Database.Save();
            return employeeDTO;
        }

        public async Task<TouragencyEmployeeDTO> Delete(int id)
        {
            TouragencyEmployee employee = await Database.TouragencyEmployees.GetById(id);
            if (employee == null)
            {
                throw new ValidationException("Така працівник вже існує", "");
            }
            var dto = await GetById(id);
            await Database.TouragencyEmployees.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetAll()
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetAll());
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> Get200Last()
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.Get200Last());
        }

        public async Task<TouragencyEmployeeDTO?> GetById(int id)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<TouragencyEmployee, TouragencyEmployeeDTO>(await Database.TouragencyEmployees.GetById(id));
        }

        public async Task<TouragencyEmployeeDTO?> GetByAccountId(int accountId)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<TouragencyEmployee, TouragencyEmployeeDTO>(await Database.TouragencyEmployees.GetByAccountId(accountId));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByFirstname(string firstname)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByFirstname(firstname));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByLastname(string lastname)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByLastname(lastname));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByMiddlename(string middlename)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByMiddlename(middlename));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByPositionName(string positionName)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByPositionName(positionName));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByPositionDescription(string positionDescription)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByPositionDescription(positionDescription));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByAccountLogin(string touragencyAccountLogin)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByAccountLogin(touragencyAccountLogin));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByAccountRoleId(int touragencyAccountRoleId)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByAccountRoleId(touragencyAccountRoleId));
        }

        public async Task<IEnumerable<TouragencyEmployeeDTO>> GetByCompositeSearch(string? firstname, string? lastname,
            string? middlename, string? positionName, string? positionDescription, string? touragencyAccountLogin, int? touragencyAccountRoleId,
            string? emailAddress, string? phoneNumber)
        {
            var mapper = new Mapper(Employee_EmployeeDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployee>, IEnumerable<TouragencyEmployeeDTO>>(await Database.TouragencyEmployees.GetByCompositeSearch(firstname, lastname, middlename, positionName, positionDescription, touragencyAccountLogin, touragencyAccountRoleId, emailAddress, phoneNumber));
        }

    }
}
