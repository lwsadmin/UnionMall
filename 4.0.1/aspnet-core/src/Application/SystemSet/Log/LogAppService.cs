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
                table = $@"select l.id, l.Content,l.UserAccount,l.IPAddress,l.CreationTime from TLog l where id in(
select id from TLog order by id desc OFFSET {(pageIndex - 1) * pageSize} ROW FETCH NEXT {pageSize} ROWS only)";
            }
            if (AbpSession.TenantId != null && (int)AbpSession.TenantId > 0)
            {
                table += $" and TenantId={AbpSession.TenantId}";
            }
            table += where;
            string idSql = "";
            if (where == "")
            {
                idSql = $"SELECT rows FROM sysindexes WHERE id = OBJECT_ID('dbo.TLOG') AND indid < 2";
            }
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total, idSql, table);
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
                l.TenantId = 1;
                l.UserAccount = "Unknown";
            }
            l.IPAddress = _Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            l.Content = content;
            await _Repository.InsertAsync(l);

        }
    }
}
