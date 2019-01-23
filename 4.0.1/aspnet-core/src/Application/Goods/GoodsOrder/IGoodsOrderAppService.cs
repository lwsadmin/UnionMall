using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Goods.GoodsOrder.Dto;

namespace UnionMall.Goods
{
    public interface IGoodsOrderAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.GoodsOrder model);
        Task<Entity.GoodsOrder> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<JsonResult> OffConsume(SubmitOrderDto dto);

        Task<MemoryStream> ExportToExcel(string where);
    }
}
