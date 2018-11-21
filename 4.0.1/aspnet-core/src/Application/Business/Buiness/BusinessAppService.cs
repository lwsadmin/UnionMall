using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Business.Dto;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
namespace UnionMall.Business
{
    public class BusinessAppService : ApplicationService, IBusinessAppService
    {
        private readonly IRepository<Entity.Business, long> _Repository;
        private readonly IRepository<Entity.ChainStore, long> _storeRepository;
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public BusinessAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.Business, long> Repository,
            IRepository<Entity.ChainStore, long> storeRepository,
        IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _storeRepository = storeRepository;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<List<BusinessDropDownDto>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }
            DataTable role = _sqlExecuter.ExecuteDataSet($"select s.Id, r.Name RoleName,r.ManageRole,r.BusinessId from dbo.TUsers s " +
    $"left join dbo.TUserRoles ur on s.Id=ur.UserId left join dbo.TRoles r on ur.RoleId = r.Id where s.id={_AbpSession.UserId}").Tables[0];
            if (role.Rows[0]["RoleName"].ToString().ToUpper() != "ADMIN")// 
            {
                query = query.FindAll(c => c.Id == (long)role.Rows[0]["BusinessId"]);
            }
            return query.MapTo<List<BusinessDropDownDto>>();
        }

        public async Task CreateOrEditAsync(Entity.Business bus)
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

        public async Task<Entity.Business> GetByIdAsync(long Id)
        {
            var s = await _Repository.GetAsync(Id);
            return s;
        }

        public bool Delete(long id, out string msg)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null)
            {
               msg = "";
                return true;
            }
            var count = _storeRepository.GetAllList(c => c.BusinessId == id).Count;
            if (count > 0)
            {
                msg = "ExistRecord";
                return false;
            }
             _Repository.Delete(id);
            msg = "";
            return true;
        }
    }
}
