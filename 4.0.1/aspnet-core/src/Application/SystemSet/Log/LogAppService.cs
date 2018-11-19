using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
using Microsoft.AspNetCore.Http;

namespace UnionMall.SystemSet
{
    public class LogAppService : ApplicationService, ILogAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Log, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public readonly IHttpContextAccessor _Accessor;
        public LogAppService(ISqlExecuter sqlExecuter,
            IRepository<Log, long> Repository, IAbpSession AbpSession, IHttpContextAccessor Accessor)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _Accessor = Accessor;
        }


        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.tenantid,m.title, cast(m.initpoint as float) initpoint,cast(m.sellprice as float) sellprice,
cast(m.minPoint as float) minPoint,cast(m.maxPoint as float) maxPoint from tmemberlevel m where 1=1 ";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and m.TenantId={_AbpSession.TenantId}";
            }
            table += where;
             return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task WriteLog(string content)
        {
            Log l = new Log();
            if (_AbpSession.TenantId != null)
            {
                l.TenantId = (int)_AbpSession.TenantId;
                DataTable user = _sqlExecuter.ExecuteDataSet($"select s.Id, s.Name from dbo.TUsers s "
    + $"where s.id={_AbpSession.UserId}").Tables[0];
                l.UserAccount = user.Rows[0]["Name"].ToString();
            }
            else
            {
                l.TenantId = 0;
                l.UserAccount = "Unknown";
            }
            l.IPAddress = _Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            l.Content = content;
            await _Repository.InsertAsync(l);

        }
    }
}
