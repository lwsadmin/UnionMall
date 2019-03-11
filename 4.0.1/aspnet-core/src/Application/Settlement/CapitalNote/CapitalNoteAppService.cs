using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
using UnionMall.IRepositorySql;
using UnionMall.Member;

namespace UnionMall.Settlement.CapitalNote
{
    public class CapitalNoteAppService : ApplicationService, ICapitalNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public readonly IMemberAppService _memAppServices;
        private readonly IRepository<Entity.ChainStoreCapitalNote, long> _Repository;

        public CapitalNoteAppService(ISqlExecuter sqlExecuter, IMemberAppService memAppServices,
    IRepository<Entity.ChainStoreCapitalNote, long> Repository,
    IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _memAppServices = memAppServices;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
        [RemoteService(IsEnabled = false)]
        public async Task CreateOrEditAsync(ChainStoreCapitalNote note)
        {
            await _Repository.InsertAsync(note);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select n.id,n.billNumber,c.Name,c.IsSystem,n.type,n.way,cast(n.value as float) value,cast(n.balance as float) balance,n.useraccount,n.memo,n.CreationTime 
from TChainStoreCapitalNote n left join dbo.TChainStore c
on n.ChainStoreId=c.id  where 1=1 ";
            }
            where = where.ToLower().Replace("*", "n").Replace("*.businessid", "c.businessid");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
