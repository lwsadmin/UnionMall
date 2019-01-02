using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Attribute;
using UnionMall.Common.Dtos;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpec
{
    public class CommonSpecAppService : ApplicationService, ICommonSpecAppService
    {
        private readonly IRepository<Entity.CommonSpec, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonSpecAppService(IRepository<Entity.CommonSpec, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }
        public Task CreateOrEditAsync(Entity.CommonSpec cat)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Entity.CommonSpec> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SpecDropDown>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            //  query = query.FindAll(c => c.BusinessId == businessID);
            return query.MapTo<List<SpecDropDown>>();
        }

        public Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select s.Id,s.Name,
stuff((select  i.Name +','  from dbo.TCommonSpecValue i
 left join dbo.TCommonSpec f on i.SpecId=f.Id
 where s.Id=f.Id
for xml path('')),1,1,'') VName
from TCommonSpec s
where s.TenantId={AbpSession.TenantId} {where}  order by id OFFSET {(pageIndex - 1) * pageSize} ROW FETCH NEXT {pageSize} ROWS only";
            }
            string idSql = $@"select count(id) from TCommonSpec  where tenantid={AbpSession.TenantId} {where.Replace("s.", "")}";
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, "1", orderBy, out total, idSql, table);
        }
    }
}
