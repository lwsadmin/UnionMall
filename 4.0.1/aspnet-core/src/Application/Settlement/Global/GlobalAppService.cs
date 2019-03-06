using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;

namespace UnionMall.Settlement.Global
{
    public class GlobalAppService : ApplicationService, IGlobalAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.GlobalSet, long> _Repository;
        public GlobalAppService(ISqlExecuter sqlExecuter, IRepository<Entity.GlobalSet, long> Repository,
            IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
            _Repository = Repository;
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.GlobalSet> GetGlobalSet()
        {
            var s = await _Repository.FirstOrDefaultAsync(c => c.TenantId == AbpSession.TenantId);
            if (s == null)
            {
                s = new Entity.GlobalSet(AbpSession.TenantId ?? 0);
                await _Repository.InsertAsync(s);
            }
            return s;
        }
        [RemoteService(IsEnabled = false)]
        public void SaveGlobal(string column, decimal value)
        {
            string sql = $@"update TGlobalSet set {column}={value} where TenantId={AbpSession.TenantId}";

            _sqlExecuter.Execute(sql);
        }
    }
}
