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
    public class LevelUpdateAppService : ApplicationService, ILevelUpdateAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.LevelUpdate, long> _Repository;
        public LevelUpdateAppService(ISqlExecuter sqlExecuter, IRepository<Entity.LevelUpdate, long> Repository, IAbpSession AbpSession)
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
                table = $@"select u.id,u.billNumber, convert( nvarchar,u.creationtime,120) creationtime,cast(WeChatPay as float) WeChatPay,
 cast(alipay as float) alipay,cast(cashpay as float) cashpay,l.Title BeforeLevel,l2.Title AfterLevel,
 stuff(m.CardID,8,4,'****') CardID, stuff(m.WeChatName,1,1,'*') WeChatName
 from TMemberLevelUpdate u left join dbo.TMemberLevel l
on u.beforeid=l.id left join dbo.TMemberLevel l2 on u.afterid=l2.id
left join dbo.TMember m on u.memberid=m.id where 1=1  ";
            }
            where = where.Replace("*.BusinessId", "m.BusinessId").Replace("*", "u");
            table += where;


            //int[] store = { 6, 16, 17, 18, 19, 20, 21 };
            //for (int i = 0; i < 4528; i++)
            //{
            //    int m = new Random().Next(1, 15049);
            //    string sql = $@"update TMemberLevelUpdate set WeChatPay={new Random().Next(30, 1000)} where id={i + 1}";
            //    _sqlExecuter.Execute(sql);
            //}

            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
