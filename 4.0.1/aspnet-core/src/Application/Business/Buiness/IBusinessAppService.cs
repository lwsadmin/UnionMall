using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Business.Buiness.Dto;

namespace UnionMall.Business.Buiness
{
    public interface IBusinessAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task<List<BusinessDropDownDto>> GetDropDown();
    }
}
