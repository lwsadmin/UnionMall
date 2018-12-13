using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
namespace UnionMall.SystemSet
{
    public interface IAdvertAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Advert dto);
        Task<Advert> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
    }
}
