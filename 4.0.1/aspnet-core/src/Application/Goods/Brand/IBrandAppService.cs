using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Goods.Brand;
namespace UnionMall.Goods.Brand
{
    public interface IBrandAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task CreateOrEditAsync(Brand dto);
        Task<Brand> GetByIdAsync(long Id);
        Task DeleteAsync(long id);

    }
}
