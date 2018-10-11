using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business.ChainStore;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Business
{
    public class ChainStoreAppService : ApplicationService, IChainStoreAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<ChainStore.ChainStore, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public ChainStoreAppService(ISqlExecuter sqlExecuter, IAbpSession AbpSession,
            IRepository<ChainStore.ChainStore, long> Repository)
        {
            _Repository = Repository;
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
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

        public async Task CreateOrEditAsync(ChainStore.ChainStore store)
        {
            if (store.Id > 0)
            {
                await _Repository.UpdateAsync(store);
            }
            else
            {
                if (_AbpSession.TenantId != null)
                { store.TenantId = (int)_AbpSession.TenantId; }
                await _Repository.InsertAsync(store);
            }
        }
    }
}
