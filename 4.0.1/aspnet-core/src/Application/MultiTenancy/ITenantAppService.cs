using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UnionMall.MultiTenancy.Dto;

namespace UnionMall.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
