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
using UnionMall.Member;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Coupon.ReceiveStatistics;

namespace UnionMall.Coupon
{
    public class CouponAppService : ApplicationService, ICouponAppService
    {

        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        public readonly IMemberAppService _memAppServices;
        private readonly IRepository<Entity.Coupon, long> _Repository;
        private readonly IReceiveStatisticsAppService _reciveNoteAppSerivece;
        public CouponAppService(ISqlExecuter sqlExecuter, IMemberAppService memAppServices,
            IRepository<Entity.Coupon, long> Repository, IReceiveStatisticsAppService reciveNoteAppSerivece,
            IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _memAppServices = memAppServices;
            _reciveNoteAppSerivece = reciveNoteAppSerivece;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
        public async Task Delete(long id)
        {
            await _Repository.DeleteAsync(c => c.Id == id);
        }
        [RemoteService(IsEnabled = false)]
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
            if (dto.Id <= 0)
            {
                await _Repository.InsertAsync(dto.MapTo<Entity.Coupon>());
            }
            else
            {
                await _Repository.UpdateAsync(dto.MapTo<Entity.Coupon>());
            }

        }
        [RemoteService(IsEnabled = false)]
        public async Task<CreateEditDto> GetByIdAsync(long Id)
        {
            return AutoMapper.Mapper.Map<CreateEditDto>(await _Repository.GetAsync(Id));
        }
        [RemoteService(IsEnabled = false)]
        public async Task<JsonResult> SendCoupon(long MemberId, long CouponId, string BillNumber = "")
        {
            var json = new JsonResult(new { });
            var coupon = _Repository.FirstOrDefault(c => c.Id == CouponId);
            if (coupon == null)
            {
                json.Value = new { succ = false, msg = L("NotExist{0}", L("Count")) };
                return json;
            }
            if (coupon.TotalCount <= 0)
            {
                json.Value = new { succ = false, msg = L("Coupon") + L("Count") + L("NEnough") };
                return json;
            }
            coupon.TotalCount -= 1;
            await _Repository.UpdateAsync(coupon);

            CouponSendStatistics cn = new CouponSendStatistics();
            cn.TenantId = coupon.TenantId;
            cn.CouponId = CouponId;
            cn.MemberId = MemberId;
            cn.SendOrderBillNumber = BillNumber;
            cn.BeginDate = DateTime.Now;
            cn.EndDate = DateTime.Now.AddDays(coupon.ValidityDay);
            await _reciveNoteAppSerivece.CreateAsync(cn);
            json.Value = new { succ= true };
            return json;
        }
    }
}
