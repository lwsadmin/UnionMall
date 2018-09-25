using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Goods.GoodsCategory.Dto;
namespace UnionMall.Goods.GoodsCategory
{
    public interface IGoodsCategoryAppService : IApplicationService
    {

        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task CreateOrEditAsync(CategoryEditDto dto);
        Task<CategoryEditDto> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<List<CategoryEditDto>> GetAllListByParentIdAsync(long parentId);
        DataSet GetCategoryDropDownList(int? tenantId, int parentId = 0, int type = 0);
    }
}
