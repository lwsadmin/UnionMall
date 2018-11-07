using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;

namespace UnionMall.Member.MemberLevel
{
    public class MemverLevelAppService : ApplicationService, IMemverLevelAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<MemberLevel, long> _Repository;
        public MemverLevelAppService(ISqlExecuter sqlExecuter, IRepository<MemberLevel, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository =Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.tenantid,m.title,m.initpoint,m.sellprice,m.minPoint,m.maxPoint from tmemberlevel m where 1=1 ";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and m.TenantId={_AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<JsonResult> CreateOrEditAsync(MemberLevel dto)
        {
            if (dto.Id > 0)
            {
                await _Repository.UpdateAsync(dto);
                return new JsonResult(new { succ = true, msg = "" });
            }
            else
            {
                await _Repository.InsertAsync(dto);
                return new JsonResult(new { succ = true, msg = "" });
            }

        }

    }
}
