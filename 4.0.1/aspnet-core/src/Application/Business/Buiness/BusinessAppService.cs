using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business.Buiness.Dto;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Buiness
{
    public class BusinessAppService : ApplicationService, IBusinessAppService
    {
        private readonly IRepository<Business, long> _Repository;
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public BusinessAppService(ISqlExecuter sqlExecuter,
            IRepository<Business, long> Repository,
            IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<List<BusinessDropDownDto>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }
            return query.MapTo<List<BusinessDropDownDto>>();
        }
    }
}
