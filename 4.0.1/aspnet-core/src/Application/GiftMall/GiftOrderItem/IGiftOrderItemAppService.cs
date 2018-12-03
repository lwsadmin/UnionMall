using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.GiftMall.Dto;

namespace UnionMall.Gift
{
    public interface IGiftOrderItemAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.GiftOrder model);
        Task<Entity.GiftOrderItem> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<List<GiftOrderDetail>> GetOrderDetail(long orderId);
    }
}
