using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Business.Business.Dto;

namespace UnionMall.Business.Business
{
    public interface IBusinessAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task<List<BusinessDropDownDto>> GetDropDown();
        Task CreateOrEditAsync(Business bus);
        Task<Business> GetByIdAsync(long Id);
        bool Delete(long id, out string msg);
    }
}
