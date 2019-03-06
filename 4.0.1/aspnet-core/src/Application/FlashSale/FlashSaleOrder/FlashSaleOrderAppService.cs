using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
using UnionMall.IRepositorySql;
using UnionMall.Common;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Drawing;

namespace UnionMall.FlashSale
{
    public class FlashSaleOrderAppService : ApplicationService, IFlashSaleOrderAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.FlashSaleOrder, long> _Repository;
        private readonly IImageAppService _imgService;
        private readonly ICommonAppService _comService;

        public FlashSaleOrderAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
            ICommonAppService comService,
    IRepository<Entity.FlashSaleOrder, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
            _comService = comService;
        }
        public async Task CreateOrEditAsync(FlashSaleOrder model)
        {
            if (model.Id >= 0)
            {
                await _Repository.UpdateAsync(model);
            }
            else
            {
                await _Repository.InsertAsync(model);
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task DeleteAsync(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.FlashSaleOrder> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select o.Id, convert(nvarchar(100),o.CreationTime,120) CreationTime,cast(o.TotalMoney as float) TotalMoney,
cast(o.TotalPay as float) TotalPay,
cast(o.WeChatPay as float) WeChatPay,
cast(o.AliPay as float) AliPay,
cast(o.BalancePay as float) BalancePay,
cast(o.IntegralPay as float) IntegralPay,
cast(o.CouponPay as float) CouponPay,
o.OrderNumber,o.Status,c.Name,stuff(m.CardID,8,4,'****') CardID,
stuff(m.WechatName,2,1,'*') WeChatName from dbo.TFlashSaleOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " o");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<MemoryStream> ExportToExcel(string where)
        {

            string sql = $@"select convert(nvarchar(100),o.CreationTime,120)  下单时间,
case  o.status when 0  then '已付款'
 when -1 then '未付款' when 2 then '已撤销'  end 状态,
 cast(o.TotalPay as float) 总计支付,
  cast(o.WeChatPay as float) 微信支付,
    cast(o.AliPay as float) 支付宝支付,
	    cast(o.BalancePay as float) 余额支付,
 cast(o.IntegralPay as float) 支付积分,
  cast(o.CouponPay as float) 优惠券积分,
o.OrderNumber 订单号,stuff((select  f.Title  +'   ￥'+convert(nvarchar,cast(i.Price as float)) +'×'+convert(nvarchar, i.Count) +'；' 
 from dbo.TFlashSaleOrderItem i
 left join dbo.TFlashSale f on i.FlashSaleId=f.Id
where i.OrderId =o.Id
for xml path('')),1,1,'') 商品信息,c.Name 门店,stuff(m.CardID,8,4,'****') 会员卡号,
m.WeChatName 微信名 from dbo.TFlashSaleOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            sql += where.Replace("*", "o");
         
            return await _comService.DataTableToExcel(sql);
        }
    }
}
