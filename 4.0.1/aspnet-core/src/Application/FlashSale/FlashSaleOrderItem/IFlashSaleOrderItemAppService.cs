using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.FlashSale.Dto;

namespace UnionMall.FlashSale
{
    public interface IFlashSaleOrderItemAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.FlashSaleOrderItem model);
        Task<Entity.FlashSaleOrderItem> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<List<OrderDetail>> GetOrderDetail(long orderId);
    }
}
