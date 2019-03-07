using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common;
using UnionMall.Common.CommonSpec;
using UnionMall.Entity;
using UnionMall.Goods.GoodsOrder.Dto;
using UnionMall.IRepositorySql;
using UnionMall.Sessions;

namespace UnionMall.Goods
{
    public class GoodsOrderAppService : ApplicationService, IGoodsOrderAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.GoodsOrder, long> _Repository;
        private readonly IImageAppService _imgService;
        private readonly ICommonAppService _comService;
        private readonly ISpecObjectAppService _objService;
        private readonly ISessionAppService _sessionAppService;
        public GoodsOrderAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
            ICommonAppService comService, ISpecObjectAppService objService, ISessionAppService sessionAppService,
    IRepository<Entity.GoodsOrder, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
            _comService = comService;
            _objService = objService;
            _sessionAppService = sessionAppService;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
        public Task CreateOrEditAsync(Entity.GoodsOrder model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task<MemoryStream> ExportToExcel(string where)
        {
            string sql = $@"select convert(nvarchar(100),o.SubmitTime,120)  下单时间,
case  o.status when 0  then '已付款'
 when -1 then '未付款' when 2 then '已撤销'  end 状态,
 cast(o.TotalPay as float) 总计支付,
  cast(o.WeChatPay as float) 微信支付,
    cast(o.AliPay as float) 支付宝支付,
	    cast(o.BalancePay as float) 余额支付,
 cast(o.IntegralPay as float) 支付积分,
  cast(o.CouponPay as float) 优惠券积分,
o.BillNumber 订单号,stuff((select  g.Name  +'   ￥'+convert(nvarchar,cast(i.Price as float)) +'×'+convert(nvarchar, i.Count) +'；' 
 from dbo.TGoodsOrderItem i
 left join dbo.TGoods g on i.GoodsId=g.Id
where i.Goodsorderid =o.Id
for xml path('')),1,1,'') 商品信息,c.Name 门店,stuff(m.CardID,8,4,'****') 会员卡号,
m.WeChatName 微信名 from dbo.TGoodsOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            sql += where.Replace("*", "c");

            return await _comService.DataTableToExcel(sql);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.GoodsOrder> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select o.Id, convert(nvarchar(100),o.SubmitTime,120) CreationTime,cast(o.TotalMoney as float) TotalMoney,
cast(o.TotalPay as float) TotalPay,
cast(o.WeChatPay as float) WeChatPay,
cast(o.AliPay as float) AliPay,
cast(o.BalancePay as float) BalancePay,
cast(o.IntegralPay as float) IntegralPay,
cast(o.CouponPay as float) CouponPay,
o.BillNumber,o.Status,c.Name,stuff(m.CardID,8,4,'****') CardID,
stuff(m.WechatName,2,1,'*') WeChatName from dbo.TGoodsOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " o");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        //[Abp.Domain.Uow.UnitOfWork]
        public async Task<JsonResult> OffConsume(SubmitOrderDto dto)
        {
            var json = new JsonResult(new { succ = true, msg = L("Consume") + L("Success") + "!" });

            //Entity.CommonSpecObject obj = new Entity.CommonSpecObject();
            //if (ObjId > 0)
            //{
            //    obj = await _objService.GetEntityById(ObjId);
            //    if (obj == null)
            //    {
            //        json.Value = new { succ = false, msg = L("NotExist{0}", "SPU") + "!" };
            //        return json;
            //    }
            //}
            var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
            //GoodsOrder order = new GoodsOrder();
            dto.Order.BillNumber = _comService.GetBillNumber(Enum.OrderNumberType.OFGD);
            dto.Order.TenantId = UserInfo.Tenant.Id;
            dto.Order.BusinessId = (long)_sqlExecuter.ExecuteDataSet($"select businessid from tchainStore where id={UserInfo.User.ChainStoreId}").Tables[0].Rows[0][0];
            dto.Order.ChainStoreId = UserInfo.User.ChainStoreId;
            //order.MemberId = MemberId;
            //order.TotalMoney = obj.Price * count;
            //order.TotalPay = obj.Price * count;
            // if()
            return json;
        }


        [RemoteService(IsEnabled = false)]
        Task<Entity.GoodsOrder> IGoodsOrderAppService.GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }
    }
}
