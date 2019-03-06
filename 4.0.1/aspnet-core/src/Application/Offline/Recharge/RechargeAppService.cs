using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;

namespace UnionMall.Offline
{
    public class RechargeAppService : ApplicationService, IRechargeAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        
        public RechargeAppService(ISqlExecuter sqlExecuter, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
  
            _AbpSession = AbpSession;
        }
        [RemoteService(IsEnabled = false)]
        public Task<JsonResult> MemberRecharge()
        {


            throw new NotImplementedException();
        }
    }
}
