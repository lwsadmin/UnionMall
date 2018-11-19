using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Entity;
using UnionMall.Coupon.Dto;
namespace UnionMall.Coupon
{
    public interface ICounponAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(CreateEditDto dt);
        Task<CreateEditDto> GetByIdAsync(long Id);

        Task Delete(long id);
    }
}
