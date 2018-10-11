using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.Business.ChainStore;

namespace UnionMall.Business.Business
{
    public interface IChainStoreAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
        Task<ChainStore.ChainStore> GetByIdAsync(long Id);
        Task CreateOrEditAsync(ChainStore.ChainStore store);
        bool Delete(long id, out string msg);
    }
}
