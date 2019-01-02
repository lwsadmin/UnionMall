using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Dto;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpec
{
    public class SpecValueAppService : ApplicationService, ISpecValueAppService
    {
        private readonly IRepository<Entity.CommonSpecValue, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public SpecValueAppService(IRepository<Entity.CommonSpecValue, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }

        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<List<ValueItemDto>> GetSelect()
        {
            var query = await _Repository.GetAllListAsync();
            return query.MapTo<List<ValueItemDto>>();
        }
    }
}
