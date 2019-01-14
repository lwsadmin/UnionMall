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
                table = $@"select n.id,n.BillNumber,cast( n.TotalPay as float) TotalPaid,
cast( n.CashPay as float) CashPay, cast( n.WeChatPay as float) WeChatPay,
cast( n.AliPay as float) AliPay,cast( n.Value as float) Value,cast( n.Balance as float) Balance,n.UserAccount
,CONVERT(nvarchar,n.CreationTime,120) CreationTime,c.Name,stuff(m.CardID,8,4,'****') CardID,stuff(m.WeChatName,2,1,'*') WeChatName from TMemberRechargeNote n left join TChainStore c
on n.ChainStoreId=c.id left join TMember m on n.MemberId=m.Id where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public DataSet GetTotalData(string where)
        {

            string table = $@"select isnull( cast(sum( TotalPay) as float),0) TotalPay,
isnull( cast(sum( AliPay) as float),0) AliPay,
isnull(cast(sum( WeChatPay) as float),0) WeChatPay,
isnull(cast(sum( CashPay) as float),0) CashPay
 from dbo.TMemberRechargeNote n left join TChainStore c on n.ChainStoreId=c.id 
 left join TMember m on n.MemberId=m.Id
where 1=1 
{  where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n")}";
            return _sqlExecuter.ExecuteDataSet(table);
        }

        public async Task Create(RechargeNote note)
        {
            await _Repository.InsertAsync(note);
        }
    }
}
