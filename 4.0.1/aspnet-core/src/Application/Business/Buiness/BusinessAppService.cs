using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business.Business.Dto;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Business
{
    public class BusinessAppService : ApplicationService, IBusinessAppService
    {
        private readonly IRepository<Business, long> _Repository;
        private readonly IRepository<ChainStore.ChainStore, long> _storeRepository;
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public BusinessAppService(ISqlExecuter sqlExecuter,
            IRepository<Business, long> Repository,
            IRepository<ChainStore.ChainStore, long> storeRepository,
        IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _storeRepository = storeRepository;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<List<BusinessDropDownDto>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }
            return query.MapTo<List<BusinessDropDownDto>>();
        }

        public async Task CreateOrEditAsync(Business bus)
        {
            if (bus.Id > 0)
            {
                await _Repository.UpdateAsync(bus);
            }
            else
            {
                if (_AbpSession.TenantId != null)
                { bus.TenantId = (int)_AbpSession.TenantId; }
                await _Repository.InsertAsync(bus);
            }
        }

        public async Task<Business> GetByIdAsync(long Id)
        {
            var s = await _Repository.GetAsync(Id);
            return s;
        }

        public bool Delete(long id, out string msg)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null)
            {
                msg = "NotExist";
                return false;
            }
            var count = _storeRepository.GetAllList(c => c.BusinessId == id).Count;
            if (count > 0)
            {
                msg = "HasChild";
                return false;
            }
            // _Repository.Delete(id);
            msg = "";
            return true;
        }
    }
}
