using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UnionMall.Roles.Dto;
using UnionMall.Users.Dto;

namespace UnionMall.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
