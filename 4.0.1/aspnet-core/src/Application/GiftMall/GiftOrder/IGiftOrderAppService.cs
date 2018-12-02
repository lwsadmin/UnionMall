using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.GiftMall.Dto;

namespace UnionMall.Gift
{
    public interface IGiftOrderAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(CreateOrEditDto model);
        Task<Entity.Gift> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
    } 
}
