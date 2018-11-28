﻿using Abp.Application.Services;
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
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select n.id, n.BillNumber,n.Type,n.TotalPaid,m.Status,n.UserAccount,n.CreationTime,
c.Name,m.CardID,m.WeChatName,c.BusinessId
 from dbo.TConsumeNote n left join dbo.TMember m 
on n.Memberid=m.id left join dbo.TChainStore c on n.ChainStoreId=c.Id where 1=1 ";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace("*", "n");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}