using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.GiftMall.Dto;

namespace UnionMall.Gift
{
    public interface IGiftOrderAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.GiftOrder model);
        Task<Entity.GiftOrder> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<JsonResult> OffExchange(ExchangDto dto);
        Task<MemoryStream> ExportToExcel(string where);
    } 
}
