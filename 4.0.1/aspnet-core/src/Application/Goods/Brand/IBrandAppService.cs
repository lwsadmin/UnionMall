using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Goods.Dto;
using UnionMall.Entity;
using System.Linq.Expressions;
namespace UnionMall.Goods
{
    public interface IBrandAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task<JsonResult> CreateOrEditAsync(Brand dto);
        Task<Brand> GetByIdAsync(long Id);
        Task<JsonResult> DeleteAsync(long id);
        List<BrandSelectDto> GetMultiSelect(long catId = 0);
    }
}
