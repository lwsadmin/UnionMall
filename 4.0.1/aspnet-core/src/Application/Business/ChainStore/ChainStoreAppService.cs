using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
using UnionMall.Business.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Caching;

namespace UnionMall.Business
{
    public class ChainStoreAppService : ApplicationService, IChainStoreAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<ChainStore, long> _Repository;
        public readonly IAbpSession _AbpSession;
        private readonly ICacheManager _cacheManager;

        public ChainStoreAppService(ISqlExecuter sqlExecuter, IAbpSession AbpSession,
            IRepository<ChainStore, long> Repository, ICacheManager cacheManager)
        {
            _Repository = Repository;
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
            _cacheManager = cacheManager;
        }

        public Task<ChainStore> GetByIdAsync(long Id)
        {
            return _Repository.GetAsync(Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.id, c.IsSystem, c.id,c.BusinessId,c.Name,b.BusinessName, c.Image,c.Mobile,c.CreationTime,c.Contact,c.Sort from TChainStore c
left join TBusiness b on c.BusinessId = b.Id where 1=1 ";
            }
            table += where.Replace("*","c");
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        public bool Delete(long id, out string msg)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null)
            {
                msg = "";
                return true;
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
        public async Task<List<StoreDropDownDto>> GetDropDown(long? businessID = null)
        {
            var query = await _Repository.GetAllListAsync();
            if (businessID == null)
            {
                DataTable role = _sqlExecuter.ExecuteDataSet($"select s.Id, r.Name RoleName,r.ManageRole,r.BusinessId from dbo.TUsers s " +
$"left join dbo.TUserRoles ur on s.Id=ur.UserId left join dbo.TRoles r on ur.RoleId = r.Id where s.id={_AbpSession.UserId}").Tables[0];
                if (role.Rows[0]["RoleName"].ToString().ToUpper() != "ADMIN")// 
                {
                    query = query.FindAll(c => c.Id == (long)role.Rows[0]["BusinessId"]);

                }

                return query.MapTo<List<StoreDropDownDto>>();
            }
            else
            {
                query = query.FindAll(c => c.BusinessId == businessID);
                return query.MapTo<List<StoreDropDownDto>>();
            }

        }
    }
}
