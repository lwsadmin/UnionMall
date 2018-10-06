using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using UnionMall.Business.ChainStore;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Buiness
{
    public class ChainStoreAppService : ApplicationService, IChainStoreAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<ChainStore.ChainStore, long> _Repository;
        public ChainStoreAppService(ISqlExecuter sqlExecuter, IRepository<ChainStore.ChainStore, long> Repository)
        {
            _Repository = Repository;
            _sqlExecuter = sqlExecuter;
        }

        public Task<ChainStore.ChainStore> GetByIdAsync(long Id)
        {
            return _Repository.GetAsync(Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
