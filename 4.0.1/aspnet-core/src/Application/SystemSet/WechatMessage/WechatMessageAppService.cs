using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;

namespace UnionMall.SystemSet
{
    public class WechatMessageAppService : ApplicationService, IWechatMessageAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.WechatMessage, long> _Repository;

        public WechatMessageAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.WechatMessage, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select w.id,w.Title,w.Content,w.isopen  from TWechatMessage w where TenantId={AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task ChangeStatus(long id)
        {
            var query = await _Repository.FirstOrDefaultAsync(c => c.Id == id);
            if (query != null)
            {
                if (query.IsOpen)
                    query.IsOpen = false;
                else
                    query.IsOpen = true;

                await _Repository.UpdateAsync(query);
            }
        }
    }
}
