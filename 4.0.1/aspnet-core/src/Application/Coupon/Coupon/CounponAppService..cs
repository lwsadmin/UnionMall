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
using Abp.AutoMapper;
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


        public async Task Delete(long id)
        {
            await _Repository.DeleteAsync(c => c.Id == id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.Id,c.TenantId,c.businessId,c.Title,c.Status,c.Value,c.MemberReceiveCount,c.Image,c.TotalCount,
 b.BusinessName from dbo.TCoupon c left join dbo.TBusiness b on c.businessId=b.Id  where 1=1 ";
            }
            where = where.Replace("*", "c");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task CreateOrEditAsync(CreateEditDto dto)
        {
            dto.TenantId = _AbpSession.TenantId ?? 0;
            if (dto.Type == 0)//通用代金券
            {
                string sql = $"select id from tbusiness where TenantId={_AbpSession.TenantId} and IsSystemBusiness=1";
                DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
                dto.BusinessId = (long)dt.Rows[0][0];
            }
            if (dto.Id > 0)
            {
                await _Repository.InsertAsync(dto.MapTo<Entity.Coupon>());
            }
            else
            {
                await _Repository.UpdateAsync(dto.MapTo<Entity.Coupon>());
            }

        }

        public async Task<CreateEditDto> GetByIdAsync(long Id)
        {
            return AutoMapper.Mapper.Map<CreateEditDto>(await _Repository.GetAsync(Id));
        }
    }
}
