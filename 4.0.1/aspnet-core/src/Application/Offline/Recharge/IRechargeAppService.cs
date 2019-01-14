using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Offline.Recharge
{
    public interface IRechargeAppService: IApplicationService
    {
        Task<JsonResult> MemberRecharge();
    }
}
