using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Gift
{
    public interface IGiftAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.Gift model);
        Task<Entity.Gift> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
    }
}
