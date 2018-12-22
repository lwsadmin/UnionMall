using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;

namespace UnionMall.SystemSet
{
    public class OperateModuleAppService : ApplicationService, IOperateModuleAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Entity.OperateModule, long> _Repository;

        public OperateModuleAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.OperateModule, long> Repository, IAbpSession AbpSession, IUnitOfWorkManager unitOfWorkManager)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m1.id,m1.title,m1.visabled,m1.linkurl,m1.sort,m1.icon,m1.memo,m1.keyname,m1.type from tOperateModule 
m1 where m1.TenantId={AbpSession.TenantId} {where}
union 
select m2.id,m2.title,m2.visabled,m2.linkurl,m2.sort,m2.icon,m2.memo,m2.keyname,m2.type from tOperateModule m2 where 
m2.TenantId=0 and m2.keyName
 not in(select keyName from tOperateModule where TenantId={AbpSession.TenantId}) {where}
 order by sort desc";
            }
            DataSet ds = _sqlExecuter.ExecuteDataSet(table);
            total = ds.Tables[0].Rows.Count;
            return ds;
        }

        public async Task ChangeStatus(long id)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = await _Repository.FirstOrDefaultAsync(c => c.Id == id);
                int s = (int)AbpSession.TenantId;
                if (query.TenantId == (int)AbpSession.TenantId)
                {
                    if (query.Visabled)
                        query.Visabled = false;
                    else
                        query.Visabled = true;

                    await _Repository.UpdateAsync(query);
                }
                else
                {
                    // query = await _Repository.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == 0);
                    Entity.OperateModule module = new Entity.OperateModule();
                    module.KeyName = query.KeyName;
                    module.Type = query.Type;
                    module.Title = query.Title;
                    module.TenantId = (int)AbpSession.TenantId;
                    module.Visabled = !query.Visabled;
                    module.Memo = query.Memo;
                    module.LinkUrl = query.LinkUrl;
                    module.Icon = query.Icon;
                    module.Sort = query.Sort;
                    await _Repository.InsertAsync(module);
                }
            }


        }
    }
}
