using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TouragencyAccountService : ITouragencyAccountService
    {
        IUnitOfWork Database;

        MapperConfiguration Account_AccountDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TouragencyEmployeeAccount, TouragencyEmployeeAccountDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Login", opt => opt.MapFrom(c => c.Login))
        .ForMember("Password", opt => opt.MapFrom(c => c.Password))
        .ForMember("TouragencyAccountRoleId", opt => opt.MapFrom(c => c.TouragencyAccountRoleId))
        .ForMember("TouragencyEmployeeId", opt => opt.MapFrom(c => c.TouragencyEmployeeId))
        );

        public TouragencyAccountService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Add(TouragencyEmployeeAccountDTO accountDTO)
        {
            var PreExistedEmployee = await Database.TouragencyAccounts.GetByLogin(accountDTO.Login);
            if (PreExistedEmployee.Any(em => em.Login == accountDTO.Login))
            {
                throw new ValidationException("Такий аккаунт вже існує", "");
            }
            var newAccount = new TouragencyEmployeeAccount
            {
                Login = accountDTO.Login,
                Password = accountDTO.Password, 
                TouragencyAccountRoleId = accountDTO.TouragencyAccountRoleId,
                TouragencyEmployeeId = accountDTO.TouragencyEmployeeId,
            };
            
            await Database.TouragencyAccounts.Create(newAccount);
            await Database.Save();
        }

        public async Task Update(TouragencyEmployeeAccountDTO accountDTO)
        {
            TouragencyEmployeeAccount account = await Database.TouragencyAccounts.GetById(accountDTO.Id);
            if (account == null)
            {
                throw new ValidationException("Такий аккаунт вже існує", "");
            }
            account.Login = accountDTO.Login;
            account.Password = accountDTO.Password;
            account.TouragencyAccountRoleId = accountDTO.TouragencyAccountRoleId;
            account.TouragencyEmployeeId = accountDTO.TouragencyEmployeeId;

            var role = await Database.TouragencyAccountRoles.GetById(account.TouragencyAccountRoleId);
            account.TouragencyAccountRole = role;

            var employee = await Database.TouragencyEmployees.GetById(account.TouragencyEmployeeId);
            account.TouragencyEmployee = employee;

            Database.TouragencyAccounts.Update(account);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            TouragencyEmployeeAccount account = await Database.TouragencyAccounts.GetById(id);
            if (account == null)
            {
                throw new ValidationException("Такий аккаунт вже існує", "");
            }
            await Database.TouragencyAccounts.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetAll()
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetAll());
        }

        public async Task<TouragencyEmployeeAccountDTO?> GetById(int id)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<TouragencyEmployeeAccount, TouragencyEmployeeAccountDTO>(await Database.TouragencyAccounts.GetById(id));
        }

        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByLogin(string login)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByLogin(login));
        }
        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByRole(string role)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByRole(role));
        }
        public async Task<IEnumerable<TouragencyEmployeeAccountDTO>> GetByEmployeeName(string employeeName)
        {
            var mapper = new Mapper(Account_AccountDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyEmployeeAccount>, IEnumerable<TouragencyEmployeeAccountDTO>>(await Database.TouragencyAccounts.GetByEmployeeName(employeeName));
        }
    }
}
