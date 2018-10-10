using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using UnionMall.Business.ChainStore;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Business
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
        public bool Delete(long id, out string msg)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null||1==1)
            {
                msg = "NotExist";
                return false;
            }
            var count = _Repository.GetAllList(c => c.BusinessId == id).Count;

            _Repository.Delete(id);
            msg = "";
            return true;
        }
    }
}
