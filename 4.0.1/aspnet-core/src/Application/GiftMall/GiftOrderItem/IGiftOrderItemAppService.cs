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
        Task CreateAsync(Entity.GiftOrderItem item);
        Task DeleteByOrderAsync(long orderId);
        Task<List<GiftOrderDetail>> GetOrderDetail(long orderId);
    }
}
