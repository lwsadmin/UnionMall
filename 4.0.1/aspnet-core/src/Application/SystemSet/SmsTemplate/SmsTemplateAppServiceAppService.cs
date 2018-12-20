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
    public class SmsTemplateAppService : ApplicationService, ISmsTemplateAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.SmsTemplate, long> _Repository;

        public SmsTemplateAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.SmsTemplate, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select t.id,t.keyname,t.title,t.template,t.isenable from TSmsTemplate t where TenantId={AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task ChangeStatus(long id)
        {
            var query = await _Repository.FirstOrDefaultAsync(c => c.Id == id);
            if (query != null)
            {
                if (query.IsEnable)
                    query.IsEnable = false;
                else
                    query.IsEnable = true;

                await _Repository.UpdateAsync(query);
            }
        }
    }
}
