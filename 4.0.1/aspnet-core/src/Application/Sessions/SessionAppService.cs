﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using UnionMall.Sessions.Dto;
using Abp.Application.Services;
namespace UnionMall.Sessions
{
    public class SessionAppService : UnionMallAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        [RemoteService(IsEnabled = false)]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
