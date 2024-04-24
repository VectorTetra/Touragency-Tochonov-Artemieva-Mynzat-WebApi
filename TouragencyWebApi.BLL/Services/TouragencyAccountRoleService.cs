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
        .ForPath(d => d.TouragencyEmployeeAccountsIds, opt => opt.MapFrom(c => c.TouragencyEmployeeAccounts != null ? c.TouragencyEmployeeAccounts.Select(b => b.Id) : new List<int>()))
        .ForPath(d => d.ClientsIds, opt => opt.MapFrom(c => c.Clients != null ? c.Clients.Select(b => b.Id) : new List<int>()))
        );

        public TouragencyAccountRoleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<TouragencyAccountRoleDTO> Add(TouragencyAccountRoleDTO entity)
        {
            var PreExistedRole = await Database.TouragencyAccountRoles.GetByName(entity.Name);
            if (PreExistedRole.Any(em => em.Name == entity.Name))
            {
                throw new ValidationException($"Така роль з вказаною назвою вже існує (roleName : {entity.Name})", "");
            }
            var newRole = new TouragencyAccountRole
            {
                Name = entity.Name,
                Description = entity.Description
            };
            foreach (var id in entity.TouragencyEmployeeAccountsIds)
            {
                var account = await Database.TouragencyAccounts.GetById(id);
                if (account == null)
                {
                    throw new ValidationException($"Такий акаунт турагента не існує (TouragencyEmployeeAccountsId : {id})", "");
                }
                newRole.TouragencyEmployeeAccounts.Add(account);
            }
            foreach (var id in entity.ClientsIds)
            {
                var client = await Database.Clients.GetById(id);
                if (client == null)
                {
                    throw new ValidationException($"Такий клієнт не існує (ClientsId : {id})", "");
                }
                newRole.Clients.Add(client);
            }
            await Database.TouragencyAccountRoles.Create(newRole);
            await Database.Save();
            entity.Id = newRole.Id;
            return entity;
        }

        public async Task<TouragencyAccountRoleDTO> Update(TouragencyAccountRoleDTO entity)
        {
            TouragencyAccountRole role = await Database.TouragencyAccountRoles.GetById(entity.Id);
            if (role == null)
            {
                throw new ValidationException($"Така роль вже існує (roleId : {entity.Id})", "");
            }
            role.Name = entity.Name;
            role.Description = entity.Description;
            role.TouragencyEmployeeAccounts.Clear();
            foreach (var id in entity.TouragencyEmployeeAccountsIds)
            {
                var account = await Database.TouragencyAccounts.GetById(id);
                if (account == null)
                {
                    throw new ValidationException($"Такий акаунт турагента не існує (TouragencyEmployeeAccountsId : {id})", "");
                }
                    role.TouragencyEmployeeAccounts.Add(account);
            }
            role.Clients.Clear();
            foreach (var id in entity.ClientsIds)
            {
                var client = await Database.Clients.GetById(id);
                if (client == null)
                {
                    throw new ValidationException($"Такий клієнт не існує (ClientsId : {id})", "");
                }
                    role.Clients.Add(client);
            }
            Database.TouragencyAccountRoles.Update(role);
            await Database.Save();
            return entity;
        }

        public async Task<TouragencyAccountRoleDTO> Delete(int id)
        {
            TouragencyAccountRole role = await Database.TouragencyAccountRoles.GetById(id);
            if (role == null)
            {
                throw new ValidationException($"Така роль вже існує (roleId : {id})", "");
            }
            var dto = await GetById(id);
            await Database.TouragencyAccountRoles.Delete(id);
            await Database.Save();
            return dto;
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
            var col = await Database.TouragencyAccountRoles.GetByName(Name);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByDescription(string Description)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByDescription(Description);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeFirstname(string EmployeeFirstname)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByEmployeeFirstname(EmployeeFirstname);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeLastname(string EmployeeLastname)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByEmployeeLastname(EmployeeLastname);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByEmployeeMiddlename(string EmployeeMiddlename)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByEmployeeMiddlename(EmployeeMiddlename);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientFirstname(string ClientFirstname)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByClientFirstname(ClientFirstname);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientLastname(string ClientLastname)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByClientLastname(ClientLastname);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByClientMiddlename(string ClientMiddlename)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByClientMiddlename(ClientMiddlename);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }

        public async Task<IEnumerable<TouragencyAccountRoleDTO>> GetByCompositeSearch(string? Name, string? Description, string? EmployeeFirstname,
                       string? EmployeeLastname, string? EmployeeMiddlename, string? ClientFirstname, string? ClientLastname, string? ClientMiddlename)
        {
            var mapper = new Mapper(AccountRole_AccountRoleDTOMapConfig);
            var col = await Database.TouragencyAccountRoles.GetByCompositeSearch(Name, Description, EmployeeFirstname, EmployeeLastname, EmployeeMiddlename, ClientFirstname, ClientLastname, ClientMiddlename);
            return mapper.Map<IEnumerable<TouragencyAccountRole>, IEnumerable<TouragencyAccountRoleDTO>>(col);
        }
    }
}
