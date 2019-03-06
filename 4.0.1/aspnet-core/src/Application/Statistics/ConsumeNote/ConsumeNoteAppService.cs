using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnionMall.IRepositorySql;

namespace UnionMall.ConsumeNote
{
    public class ConsumeNoteAppService : ApplicationService, IConsumeNoteAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.ConsumeNote, long> _Repository;
        public ConsumeNoteAppService(ISqlExecuter sqlExecuter, IRepository<Entity.ConsumeNote, long> Repository, IAbpSession AbpSession)
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
                table = $@"select n.id, n.BillNumber,n.Type,cast(n.TotalPaid as float) TotalPaid,
cast(n.wechatPay as float) WechatPay,
cast(n.AliPay as float) AliPay,
cast(n.CouponPay as float) CouponPay,
cast(n.BalancePay as float) BalancePay,
cast(n.IntegralPay as float) IntegralPay,
cast(n.CashPay as float) CashPay,
 n.Status,n.UserAccount,CONVERT(nvarchar(100), n.CreationTime,120) CreationTime,
c.Name,stuff(m.CardID,8,4,'****') CardID,stuff(m.WeChatName,2,1,'*') WeChatName,c.BusinessId
 from dbo.TConsumeNote n left join dbo.TMember m 
on n.Memberid=m.id left join dbo.TChainStore c on n.ChainStoreId=c.Id where 1=1  ";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetTotalData(string where)
        {

            string table = $@"select n.Type, cast(sum( TotalPaid) as float) TotalPaid
 from dbo.TConsumeNote n left join dbo.TMember m 
on n.Memberid=m.id left join dbo.TChainStore c on n.ChainStoreId=c.Id where 1=1 and n.Status!=-1
{  where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n")}
group by n.Type";
            return _sqlExecuter.ExecuteDataSet(table);
        }
    }
}
