using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Cateogory.Dto;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.Cateogory
{
    public class CommonCategoryAppService : ApplicationService, ICommonCategoryAppService
    {

        private readonly IRepository<CommonCategory, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonCategoryAppService(IRepository<CommonCategory, long> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _sqlExecuter = sqlExecuter;
        }
        public Task CreateOrEditAsync(CommonCategory cat)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            await _Repository.DeleteAsync(c => c.Id == id);
        }

        public Task<List<CommonCategory>> GetAllListByParentIdAsync(long parentId)
        {
            throw new NotImplementedException();
        }

        public Task<CommonCategory> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public List<CatDropDownDto> GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.title,c.id,c.sort,b.BusinessName from TCommonCategory c left join TBusiness b on c.businessid=b.id
where 1=1 ";
            }
            where = where.Replace(" *", "a");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
