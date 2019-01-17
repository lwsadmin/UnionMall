using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Goods.Dto;
using UnionMall.Entity;
namespace UnionMall.Goods
{
    public interface IGoodsCategoryAppService : IApplicationService
    {

        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task CreateOrEditAsync(CategoryEditDto dto);
        Task<CategoryEditDto> GetByIdAsync(long Id);
        bool Delete(long id,out string msg);
        Task<List<CategoryEditDto>> GetAllListByParentIdAsync(long parentId);
        Task<DataTable> GetGoodsCategory();
        List<DropDownDto> GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0);
    }
}
