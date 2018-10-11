using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using UnionMall.Authorization;
using UnionMall.Authorization.Roles;
using UnionMall.Authorization.Users;
using UnionMall.Roles.Dto;
using Abp.AutoMapper;
using UnionMall.EntityFrameworkCore;
using System.Data;
using UnionMall.IRepositorySql;
using Abp.Runtime.Session;

namespace UnionMall.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public RoleAppService(IRepository<Role> repository, RoleManager roleManager,
            UserManager userManager, ISqlExecuter sqlExecuter, IAbpSession AbpSession)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
        }

        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task<RoleDto> Update(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions);
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        //lws---------------------------

        public DataSet GetRole(long id)
        {
            string sql = $"select r.id,r.TenantId,r.DisplayName from dbo.TUserRoles ur left join dbo.TRoles r on ur.RoleId=r.Id where ur.UserId={id}";
            //DataSet t1 = _sqlExecuter.ExecuteDataSet(sql, null);
            //DataTable t = _sqlExecuter.ExecuteDataSet(sql, null).Tables[0];
            return _sqlExecuter.ExecuteDataSet(sql, null);
        }
        public DataSet GetRolePage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }
        public async Task<List<RoleDropDownDto>> GetDropDown()
        {
            var query = await Repository.GetAllListAsync();
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }
            return query.MapTo<List<RoleDropDownDto>>();
        }
    }
}
