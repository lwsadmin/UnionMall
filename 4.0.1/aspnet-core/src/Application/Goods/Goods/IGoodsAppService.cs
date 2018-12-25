using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Goods
{
    public interface IGoodsAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.Goods model);
        Task<Entity.Goods> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
    }
}
