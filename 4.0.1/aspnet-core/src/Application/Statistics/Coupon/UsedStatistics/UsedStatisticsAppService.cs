using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Coupon.ReceiveStatistics
{
    public class UsedStatisticsAppService : ApplicationService, IUsedStatisticsAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.CouponUsedStatistics, long> _Repository;
        public UsedStatisticsAppService(ISqlExecuter sqlExecuter, IRepository<Entity.CouponUsedStatistics, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public Task CreateAsync(CouponUsedStatistics dt)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select u.Id, u.UseTime,c.Name, p.Title,u.BillNumber,u.BillValue,
u.UseCount,m.CardID,m.WeChatName,c.BusinessId from TCouponUsedStatistics	 u
left join TChainStore c on u.ChainStoreId=c.Id
left join TCoupon p on u.CouponId=p.id
left join TMember m on u.MemberId=m.id";
            }
           // where = where.Replace("*", "s");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
