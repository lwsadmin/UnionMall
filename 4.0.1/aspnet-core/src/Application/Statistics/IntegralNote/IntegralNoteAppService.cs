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

namespace UnionMall.IntegralNote
{
    public class IntegralNoteAppService : ApplicationService, IIntegralNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.IntegralNote, long> _Repository;
        public IntegralNoteAppService(ISqlExecuter sqlExecuter, IRepository<Entity.IntegralNote, long> Repository, IAbpSession AbpSession)
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
                table = $@"select n.id,c.Name,m.WeChatName,m.CardID,n.Type,n.Way,n.BillNumber,
cast( n.Balance as float) Balance ,cast( n.Point as float) Point, n.CreationTime,n.Memo  from dbo.tIntegralNote n left  join dbo.TMember m 
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
cast(sum(Point) as float) TotalValue
 from dbo.TIntegralNote n left join dbo.TChainStore c on n.ChainStoreId=c.Id where DATEADD(YEAR,1,n.CreationTime)>=GETDATE()  
{  where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n")}
group by  SUBSTRING( CONVERT(varchar(100), n.CreationTime, 112),0,7)";
            return _sqlExecuter.ExecuteDataSet(table);
        }
        [RemoteService(IsEnabled = false)]
        public async Task CreateAsync(Entity.IntegralNote note)
        {
            await _Repository.InsertAsync(note);
        }
    }
}
