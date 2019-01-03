using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.CommonSpec;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpecObject
{
    public class SpecObjectAppService : ApplicationService, ISpecObjectAppService
    {
        private readonly IRepository<Entity.CommonSpecObject, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public SpecObjectAppService(IRepository<Entity.CommonSpecObject, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }
        public async Task AddOrEdit(Entity.CommonSpecObject value)
        {
            if (value.Id > 0)
            {
                await _Repository.UpdateAsync(value);
            }
            else
            {
                await _Repository.InsertAsync(value);
            }
        }

        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<List<Entity.CommonSpecObject>> GetByObjectId(long id, int Type = 0)
        {

            return await _Repository.GetAllListAsync(c => c.ObjectId == id && c.Type == Type);
        }

        public async Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0)
        {
            StringBuilder str = new StringBuilder();

            return str.ToString();
        }
    }
}
