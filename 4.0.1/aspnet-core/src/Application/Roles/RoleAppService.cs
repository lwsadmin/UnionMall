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
using System.Xml;
using System.IO;

namespace UnionMall.Roles
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
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
            if (string.IsNullOrEmpty(input.DisplayName))
            {
                input.DisplayName = input.Name;
            }
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
            if (string.IsNullOrEmpty(input.DisplayName))
            {
                input.DisplayName = input.Name;
            }
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
        public async Task DeleteServices(int Id)
        {
            CheckDeletePermission();
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);
            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }
            CheckErrors(await _roleManager.DeleteAsync(role));
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
        //lws---------------------------
        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = new List<PermissionDto>();
            var ManageRole = new List<RoleDto>();
            var role = new Role();
            if (input != null && input.Id > 0)
                role = await _roleManager.GetRoleByIdAsync(input.Id);
            else
            {
                role.Name = "";
                role.Id = 0;
                role.TenantId = AbpSession.TenantId;
            }

            if (_AbpSession.TenantId == null || (int)_AbpSession.TenantId == 0)
            {
                //宿主登录，显示所有权限,显示所有可管理角色
                permissions = GetPermissionDtos();
                ManageRole = _roleManager.Roles.Where(c => c.TenantId == (int)_AbpSession.TenantId && c.Id != role.Id && c.Name.ToUpper() != "ADMIN").MapTo<List<RoleDto>>();
            }
            if (permissions.Count == 0 && _AbpSession.UserId != null)
            {
                DataTable roleT = _sqlExecuter.ExecuteDataSet($"select id, Name,ManageRole from dbo.TRoles where id=" +
    $"(select RoleId from dbo.TUserRoles where UserId={_AbpSession.UserId})" +
    $" and TenantId={_AbpSession.TenantId}").Tables[0];

                if (roleT.Rows[0]["Name"].ToString().ToUpper() == "ADMIN")// //运营商总部管理员角色登录，显示所有权限,可管理角色
                {
                    permissions = GetPermissionDtos();
                    ManageRole = _roleManager.Roles.Where(c => c.TenantId == (int)_AbpSession.TenantId && c.Id != role.Id && c.Name.ToUpper() != "ADMIN").MapTo<List<RoleDto>>();
                }
                else//其他角色登录，显示对应的权限，可管理角色
                {
                    string mrStr = roleT.Rows[0]["ManageRole"].ToString();
                    var sns = PermissionManager.GetAllPermissions();

                    //    permissions
                    List<PermissionDto> listPer = new List<PermissionDto>();
                    string perSql = $"select id, Name from dbo.TPermissions where RoleId ={roleT.Rows[0]["id"]}";
                    DataTable perso = _sqlExecuter.ExecuteDataSet(perSql).Tables[0];
                    foreach (DataRow item in perso.Rows)
                    {
                        PermissionDto dto = new PermissionDto();
                        dto.Id = (long)item["id"];
                        dto.Name = item["Name"].ToString();
                        dto.DisplayName= item["Name"].ToString();
                        listPer.Add(dto);
                    }
                    permissions = listPer;

                    //   permissions =PermissionManager.GetAllPermissions() as List<PermissionDto>;
                    ManageRole = _roleManager.Roles.Where(c => mrStr.Contains(c.Id.ToString()) && c.Id != role.Id && c.Name.ToUpper() != "ADMIN").MapTo<List<RoleDto>>();
                }
            }
            var hasPermission = new List<string>();
            if (input.Id > 0)
            {
                var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                hasPermission = grantedPermissions.Select(p => p.Name).ToList();
            }

            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions),
                GrantedPermissionNames = hasPermission,
                ManageRole = ManageRole
            };
        }

        private List<PermissionDto> GetPermissionDtos()
        {
            XmlDocument NavigationXml = new XmlDocument();
            string currentDirectory = Path.GetFullPath("../../Domain/Localization/XmlData/Navigation.xml");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //忽略注释
            XmlReader reader = XmlReader.Create(currentDirectory, settings);
            NavigationXml.Load(reader);
            XmlNodeList List = NavigationXml.SelectNodes("//Navigation//First");

            List<PermissionDto> dtoList = new List<PermissionDto>();
            foreach (XmlNode item in List)
            {
                PermissionDto FirstMenudto = new PermissionDto();
                FirstMenudto.Name = item.Attributes["Name"].Value;
                FirstMenudto.DisplayName = item.Attributes["Name"].Value;
                dtoList.Add(FirstMenudto);
                if (item.ChildNodes != null && item.ChildNodes.Count > 0)
                {
                    foreach (XmlNode subItem in item.ChildNodes)
                    {
                        PermissionDto secondMenudto = new PermissionDto();
                        secondMenudto.Name = item.Attributes["Name"].Value + "." + subItem.Attributes["Name"].Value;
                        secondMenudto.DisplayName = subItem.Attributes["Name"].Value;
                        dtoList.Add(secondMenudto);
                        if (subItem.ChildNodes.Count > 0)
                        {
                            foreach (XmlNode actionItem in subItem.ChildNodes)
                            {
                                PermissionDto actionDto = new PermissionDto();
                                actionDto.Name = item.Attributes["Name"].Value + "." +
                                    subItem.Attributes["Name"].Value + "." + actionItem.Attributes["Name"].Value;
                                actionDto.DisplayName = actionItem.Attributes["Name"].Value;
                                dtoList.Add(actionDto);
                            }

                        }
                    }
                }
            }
            return dtoList;
        }


        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {


            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));

        }
        public DataSet GetRole(long id)
        {
            string sql = $"select r.id,r.TenantId,r.DisplayName from dbo.TUserRoles ur left join dbo.TRoles r on ur.RoleId=r.Id where ur.UserId={id}";
            //DataSet t1 = _sqlExecuter.ExecuteDataSet(sql, null);
            //DataTable t = _sqlExecuter.ExecuteDataSet(sql, null).Tables[0];
            return _sqlExecuter.ExecuteDataSet(sql, null);
        }
        public DataSet GetRolePage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select r.Id,r.CreationTime,r.Description,r.DisplayName,r.Name,r.IsDefault from TRoles r where r.IsDeleted=0";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                table += $" and  r.TenantId={_AbpSession.TenantId}";
                DataTable roleT = _sqlExecuter.ExecuteDataSet($"select Name,ManageRole from dbo.TRoles where id=" +
                    $"(select RoleId from dbo.TUserRoles where UserId={_AbpSession.UserId})" +
                    $" and TenantId={_AbpSession.TenantId}").Tables[0];

                if (roleT.Rows[0]["Name"].ToString().ToUpper() != "ADMIN")// 不是宿主或者超管登录
                {
                    table += $" and  r.id in({roleT.Rows[0]["ManageRole"].ToString()})";
                }
            }
            table += where;
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
