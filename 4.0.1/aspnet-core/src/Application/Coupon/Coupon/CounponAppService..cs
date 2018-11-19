using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using UnionMall.Coupon.Dto;
using UnionMall.Entity;
using UnionMall.IRepositorySql;
namespace UnionMall.Coupon
{
    public class CounponAppService : ApplicationService, ICounponAppService
    {

        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<Entity.Coupon, long> _Repository;
        public CounponAppService(ISqlExecuter sqlExecuter, IRepository<Entity.Coupon, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public Task CreateOrEditAsync(Entity.Coupon dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.Id,c.TenantId,c.businessId,c.Title,c.Status,c.Value,c.MemberReceiveCount,c.Image,c.TotalCount,
 b.BusinessName from dbo.TCoupon c left join dbo.TBusiness b on c.businessId=b.Id m where 1=1 ";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and m.TenantId={_AbpSession.TenantId}";
            }
            where = where.Replace("*", "c");
            table += where;
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public Task CreateOrEditAsync(CreateEditDto dt)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateEditDto> GetByIdAsync(long Id)
        {
            return AutoMapper.Mapper.Map<CreateEditDto>(await _Repository.GetAsync(Id));
        }
    }
}
