using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.IRepositorySql;

namespace UnionMall.Goods.Brand
{
    class BrandAppService : ApplicationService, IBrandAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Brand, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public BrandAppService(ISqlExecuter sqlExecuter,
            IRepository<Brand, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public async Task CreateOrEditAsync(Brand dto)
        {
            if (dto.Id > 0)
            {
                var query = _Repository.Get(dto.Id);
                if (query == null)
                {

                }
                await _Repository.UpdateAsync(dto);
            }
            else
            {
                var query = _Repository.FirstOrDefault(c => c.Title == dto.Title);
                if (query != null)
                {

                }

                if (_AbpSession.TenantId != null)
                { dto.TenantId = (int)_AbpSession.TenantId; }
                await _Repository.InsertAsync(dto);
            }

        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
