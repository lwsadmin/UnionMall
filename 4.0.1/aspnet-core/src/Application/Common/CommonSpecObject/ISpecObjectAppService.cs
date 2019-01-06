using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Common.CommonSpec
{
    public interface ISpecObjectAppService : IApplicationService
    {

        Task<List<Entity.CommonSpecObject>> GetByObjectId(long id, int Type = 0);
        Task AddOrEdit(Entity.CommonSpecObject value);
        Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0);
        Task Delete(Expression<Func<Entity.CommonSpecObject, bool>> c);
    }
}
