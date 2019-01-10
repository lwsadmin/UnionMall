using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Goods.Dto;
namespace UnionMall.Goods
{
    public interface IGoodsOrderItemAppService : IApplicationService
    {
        Task CreateOrEditAsync(Entity.GoodsOrderItem model);
        Task<Entity.GoodsOrderItem> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<List<GoodsOrderDetail>> GetOrderDetail(long orderId);
    }
}
