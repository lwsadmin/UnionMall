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
    public class RechargeNoteAppService : ApplicationService, IRechargeNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.RechargeNote, long> _Repository;
        public RechargeNoteAppService(ISqlExecuter sqlExecuter, IRepository<Entity.RechargeNote, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
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
    }
}
