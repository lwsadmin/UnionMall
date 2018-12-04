using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.GiftMall.Dto;

namespace UnionMall.FlashSale
{
    public interface IFlashSaleOrderAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.FlashSaleOrder model);
        Task<Entity.FlashSaleOrder> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<MemoryStream> ExportToExcel(string where);
    } 
}
