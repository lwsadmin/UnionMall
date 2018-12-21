using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnionMall.IRepositorySql;

namespace UnionMall.Statistics
{
    public class WeChatPayNoteAppService : ApplicationService, IWeChatPayNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.WeChatPayNote, long> _Repository;
        public WeChatPayNoteAppService(ISqlExecuter sqlExecuter, IRepository<Entity.WeChatPayNote, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select n.id,n.BillNumber,cast( n.amount as float) amount,
CONVERT(nvarchar,n.CreationTime,120) CreationTime,n.memo  from dbo.TWechatpaynote n 
left join dbo.TChainStore c on n.chainstoreid=c.id where 1=1 ";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public DataSet GetTotalData(string where)
        {
            string table = $@"select  SUBSTRING( CONVERT(varchar(100),n.CreationTime, 112),0,7) [Time], 
cast(sum( amount) as float) TotalValue
 from dbo.TWechatpaynote n left join dbo.TChainStore c on n.ChainStoreId=c.Id where DATEADD(YEAR,1,n.CreationTime)>=GETDATE()
{  where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n")}  
group by  SUBSTRING( CONVERT(varchar(100), n.CreationTime, 112),0,7)";
            return _sqlExecuter.ExecuteDataSet(table);
        }
    }
}
