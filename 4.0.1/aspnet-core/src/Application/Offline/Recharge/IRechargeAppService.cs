using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Offline
{
    public interface IRechargeAppService: IApplicationService
    {
        Task<JsonResult> MemberRecharge(Entity.RechargeNote note);
    }
}
