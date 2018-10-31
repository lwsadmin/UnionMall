using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business.Business;
//using UnionMall.Business.ChainStore;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.ChainStore
{
    public class ChainStoreAppService : ApplicationService, IChainStoreAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<ChainStore, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public ChainStoreAppService(ISqlExecuter sqlExecuter, IAbpSession AbpSession,
            IRepository<ChainStore, long> Repository)
        {
            _Repository = Repository;
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
        }

        public Task<ChainStore> GetByIdAsync(long Id)
        {
            return _Repository.GetAsync(Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.IsSystem, c.id,c.BusinessId,c.Name,b.BusinessName, c.Image,c.Mobile,c.CreationTime,c.Contact,c.Sort from TChainStore c
left join TBusiness b on c.BusinessId = b.Id where 1=1 ";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                table += $"  and c.TenantId={_AbpSession.TenantId}";
            }
            if (!string.IsNullOrEmpty(where))
            {
                table += where;
            }
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }
        public bool Delete(long id, out string msg)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null)
            {
                msg = "NotExist";
                return false;
            }
            // var count = _Repository.GetAllList(c => c.BusinessId == id).Count;

            _Repository.Delete(id);
            msg = "";
            return true;
        }

        public async Task CreateOrEditAsync(ChainStore store)
        {
            try
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
            catch (Exception e)
            {

                throw new Abp.UI.UserFriendlyException(e.Message);
            }

        }
    }
}
