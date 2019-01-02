using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Dto;

using UnionMall.Entity;
namespace UnionMall.Common.CommonSpec
{
    public interface ICommonSpecAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(CreateOrEdit cat);
        Task<CreateOrEdit> GetByIdAsync(long Id);
        Task Delete(long id);
        Task<List<SpecDropDown>> GetDropDown();
        Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0);
    }
}
