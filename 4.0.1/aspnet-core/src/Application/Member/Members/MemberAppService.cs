using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
namespace UnionMall.Member
{
    public class MemberAppService : ApplicationService, IMemberAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<UnionMall.Entity.Member, long> _Repository;
        public MemberAppService(ISqlExecuter sqlExecuter, IRepository<UnionMall.Entity.Member, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.name,m.TenantId,m.levelId,m.HeadImg,m.Sex,m.CardID,m.Mobile,m.Balance,m.Integral,
m.RegTime,m.businessId,m.chainStoreId,l.Title,b.BusinessName,c.Name StoreName from dbo.TMember m
left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TBusiness b on
m.businessId=b.Id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";
            }
            table += where.Replace("*", "m");
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task CreateOrEditAsync(UnionMall.Entity.Member dto)
        {
            dto.TenantId = _AbpSession.TenantId;
            if (dto.Id > 0)
            {
                await _Repository.UpdateAsync(dto);
                // return new JsonResult(new { succ = true, msg = "" });
            }
            else
            {
                await _Repository.InsertAsync(dto);
                //  return new JsonResult(new { succ = true, msg = "" });
            }

        }

        public async Task DeleteAsync(long Id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == Id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

    }
}
