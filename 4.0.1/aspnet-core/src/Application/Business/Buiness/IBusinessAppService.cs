using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Business.Dto;
using UnionMall.Entity;
namespace UnionMall.Business
{
    public interface IBusinessAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task<List<BusinessDropDownDto>> GetDropDown();
        Task CreateOrEditAsync(Entity.Business bus);
        Task<Entity.Business> GetByIdAsync(long Id);
        bool Delete(long id, out string msg);
    }
}
