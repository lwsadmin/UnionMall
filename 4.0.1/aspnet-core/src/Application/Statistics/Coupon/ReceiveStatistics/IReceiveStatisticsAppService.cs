using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace UnionMall.Coupon.ReceiveStatistics
{
    public interface IReceiveStatisticsAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateAsync(Entity.CouponSendStatistics dt);
        DataTable  GetUseableCoupon(long memberId, decimal consumeValue);
    }
}
