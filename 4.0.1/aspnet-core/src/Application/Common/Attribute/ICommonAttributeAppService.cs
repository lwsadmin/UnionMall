using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;

namespace UnionMall.Common.Attribute
{
    public interface ICommonAttributeAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(CommonAttribute cat);
        Task<CommonAttribute> GetByIdAsync(long Id);
        Task Delete(long id);
        Task<string> GetHtmlAttr(long categoryId);
    }
}
