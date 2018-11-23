﻿using System;
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
    public class ReceiveStatisticsAppService : ApplicationService, IReceiveStatisticsAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.CouponSendStatistics, long> _Repository;
        public ReceiveStatisticsAppService(ISqlExecuter sqlExecuter, IRepository<Entity.CouponSendStatistics, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public Task CreateAsync(CouponSendStatistics dt)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select s.id, c.Title,m.WeChatName,m.CardID, dateadd(day,c.ValidityDay,s.SendTime) OverDueTime, s.ReceiveCount,s.UsedCount, s.SendTime from dbo.TCouponSendStatistics s left join dbo.TMember m 
on s.MemberId=m.Id left join  dbo.TCoupon c on s.CouponId=c.Id where 1=1 ";
            }
            where = where.Replace("*", "s");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}