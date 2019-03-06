using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
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

        public async Task Delete(long id,int type)
        {

            await _Repository.DeleteAsync(c=>c.ObjectId==id&&c.Type== type);

        }
        [RemoteService(IsEnabled = false)]
        public async Task<List<Entity.CommonSpecObject>> GetByObjectId(long id, int Type = 0)
        {

            return await _Repository.GetAllListAsync(c => c.ObjectId == id && c.Type == Type);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.CommonSpecObject> GetEntityById(long id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == id);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0)
        {
            StringBuilder str = new StringBuilder();

            return str.ToString();
        }
        [RemoteService(IsEnabled = false)]
        public async Task<DataTable> GetObjTableBuyObjId(long objId, int type = 0)
        {

            string sql = $@"select o.id,o.TenantId,o.Stock,o.SellCount,o.Price,o.RetailPrice, t.Text from TCommonSpecObject o  
cross apply (select stuff((select v.Text+',' from dbo.TCommonSpecValue v 
where charindex(cast( v.Id as nvarchar(100)),cast( o.ValueText as nvarchar(1000)))>0 for xml path('')),1,1,'') as  Text ) T
where o.ObjectId={objId} and type={type}";

            return _sqlExecuter.ExecuteDataSet(sql).Tables[0];
        }
    }
}
