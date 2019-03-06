using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnionMall.IRepositorySql;

namespace UnionMall.BalanceNote
{
    public class BalanceNoteAppService : ApplicationService, IBalanceNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.BalanceNote, long> _Repository;
        public BalanceNoteAppService(ISqlExecuter sqlExecuter, IRepository<Entity.BalanceNote, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select n.id,n.UserAccount,c.Name,stuff(m.WeChatName,2,1,'*') WeChatName,stuff(m.CardID,8,4,'****') CardID,n.Type,n.Way,n.BillNumber,cast( n.Balance as float) Balance,
cast( n.Value as float) Value,n.Memo,CONVERT(nvarchar(100), n.CreationTime,120) CreationTime from dbo.TBalanceNote n left  join dbo.TMember m 
on n.MemberId=m.id left join dbo.TChainStore c on n.ChainStoreId=c.id where 1=1  ";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetStatisticsData(string where)
        {
            string table = $@"select  SUBSTRING( CONVERT(varchar(100),n.CreationTime, 112),0,7) [Time], 
cast(sum( Value) as float) TotalValue
 from dbo.TBalanceNote n left join dbo.TChainStore c on n.ChainStoreId=c.Id where DATEADD(YEAR,1,n.CreationTime)>=GETDATE()  
{  where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n")}
group by  SUBSTRING( CONVERT(varchar(100), n.CreationTime, 112),0,7)";
            return _sqlExecuter.ExecuteDataSet(table);
        }
    }
}
