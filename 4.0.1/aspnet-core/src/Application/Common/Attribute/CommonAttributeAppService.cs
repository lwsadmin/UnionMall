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

namespace UnionMall.Common.Attribute
{
    public class CommonAttributeAppService : ApplicationService, ICommonAttributeAppService
    {
        private readonly IRepository<CommonAttribute, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonAttributeAppService(IRepository<CommonAttribute, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }
        public Task CreateOrEditAsync(CommonAttribute cat)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<CommonAttribute> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select a.Id,a.Name,a.ValueName,a.DataType,a.DefultValue,a.Options, c.Title from TCommonAttribute a left join TGoodsCategory c
on a.CategoryId=c.Id
where a.TenantId={AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
