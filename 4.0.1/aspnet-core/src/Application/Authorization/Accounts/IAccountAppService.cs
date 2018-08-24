using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Authorization.Accounts.Dto;

namespace UnionMall.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
