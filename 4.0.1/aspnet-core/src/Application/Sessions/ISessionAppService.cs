using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Sessions.Dto;

namespace UnionMall.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
