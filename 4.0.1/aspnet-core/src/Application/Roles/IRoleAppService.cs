using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UnionMall.Roles.Dto;

namespace UnionMall.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);
        Task<List<RoleDropDownDto>> GetDropDown(long? businessId = null);
        DataSet GetRole(long id);
        DataSet GetRolePage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
    }
}