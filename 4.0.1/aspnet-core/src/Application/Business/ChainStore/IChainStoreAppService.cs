using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Entity;
namespace UnionMall.Business
{
    public interface IChainStoreAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task<ChainStore> GetByIdAsync(long Id);
        Task CreateOrEditAsync(ChainStore store);
        Task<List<StoreDropDownDto>> GetDropDown(long? businessID = null);
        bool Delete(long id, out string msg);
    }
}
