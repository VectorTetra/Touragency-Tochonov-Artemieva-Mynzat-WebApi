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
    public class TouragencyAccountRoleService : ITouragencyAccountRoleService
    {
        IUnitOfWork Database;

        MapperConfiguration AccountRole_AccountRoleDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TouragencyAccountRole, TouragencyAccountRoleDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForPath(d => d.TouragencyEmployeeAccountsIds, opt => opt.MapFrom(c => c.TouragencyEmployeeAccounts.Select(b => b.Id)))
        .ForPath(d => d.ClientsIds, opt => opt.MapFrom(c => c.Clients.Select(b => b.Id)))
        );

        public TouragencyAccountRoleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Add(TouragencyAccountRoleDTO entity)
        {
            var PreExistedRole = await Database.TouragencyAccountRoles.GetByName(entity.Name);
            if (PreExistedRole.Any(em => em.Name == entity.Name))
            {
                throw new ValidationException("Така роль вже існує", "");
            }
            var newRole = new TouragencyAccountRole
            {
                Name = entity.Name,
                Description = entity.Description
            };
            foreach (var id in entity.TouragencyEmployeeAccountsIds)
            {
                var account = await Database.TouragencyAccounts.GetById(id);
                if (account != null)
                {
                    newRole.TouragencyEmployeeAccounts.Add(account);
                }
            }
            foreach (var id in entity.ClientsIds)
            {
                var client = await Database.Clients.GetById(id);
                if (client != null)
                {
                    newRole.Clients.Add(client);
                }
            }
            await Database.TouragencyAccountRoles.Create(newRole);
            await Database.Save();
        }

        public async Task Update(TouragencyAccountRoleDTO entity)
        {
            TouragencyAccountRole role = await Database.TouragencyAccountRoles.GetById(entity.Id);
            if (role == null)
            {
                throw new ValidationException("Така роль вже існує", "");
            }
            role.Name = entity.Name;
            role.Description = entity.Description;
            role.TouragencyEmployeeAccounts.Clear();
            foreach (var id in entity.TouragencyEmployeeAccountsIds)
            {
                var account = await Database.TouragencyAccounts.GetById(id);
                if (account != null)
                {
                    role.TouragencyEmployeeAccounts.Add(account);
                }
            }
            role.Clients.Clear();
            foreach (var id in entity.ClientsIds)
            {
                var client = await Database.Clients.GetById(id);
                if (client != null)
                {
                    role.Clients.Add(client);
                }
            }
            Database.TouragencyAccountRoles.Update(role);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            TouragencyAccountRole role = await Database.TouragencyAccountRoles.GetById(id);
            if (role == null)
            {
                throw new ValidationException("Така роль вже існує", "");
            }
            await Database.TouragencyAccountRoles.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetAll()
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(await Database.TouragencyAccountRoles.GetAll());
        }

        public async Task<TouragencyAccountRoleDTO?> GetById(int id)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            return mapper.Map<TouragencyAccountRole, TouragencyAccountRoleDTO>(await Database.TouragencyAccountRoles.GetById(id));
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByName(string Name)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(await Database.TouragencyAccountRoles.GetByName(Name));
        }
        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeName(string employeeName)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(await Database.TouragencyAccountRoles.GetByEmployeeName(employeeName));
        }
        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientName(string clientName)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(await Database.TouragencyAccountRoles.GetByClientName(clientName));
        }
    }
}
