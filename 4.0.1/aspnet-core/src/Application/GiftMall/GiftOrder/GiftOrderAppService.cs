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
using UnionMall.GiftMall.Dto;
using UnionMall.Common;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Drawing;

namespace UnionMall.Gift
{
    public class GiftOrderAppService : ApplicationService, IGiftOrderAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.GiftOrder, long> _Repository;
        private readonly IImageAppService _imgService;
        private readonly ICommonAppService _comService;

        public GiftOrderAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
            ICommonAppService comService,
    IRepository<Entity.GiftOrder, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
            _comService = comService;
        }
        public async Task CreateOrEditAsync(GiftOrder model)
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

        public async Task DeleteAsync(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<Entity.GiftOrder> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select o.Id, convert(nvarchar(100),o.CreationTime,120) CreationTime,cast(o.Point as float) Point,
o.BillNumber,o.Status,o.ReceiveTime,o.Memo,c.Name,stuff(m.CardID,8,4,'****') CardID,stuff(m.WechatName,2,1,'*') WeChatName from dbo.TGiftOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " o");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task<MemoryStream> ExportToExcel(string where)
        {

            string sql = $@"select convert(nvarchar(100),o.CreationTime,120)  下单时间,cast(o.Point as float) 支付积分,
o.BillNumber 订单号,stuff((select  g.Name  +'   ￥'+convert(nvarchar,cast(i.Point as float)) +'×'+convert(nvarchar, i.Count) +'；' 
 from dbo.TGiftOrderItem i
 left join dbo.TGift g on i.GiftId=g.Id
where i.GiftOrderId =o.Id
for xml path('')),1,1,'') 礼品信息,
o.ReceiveTime 领取时间,o.Memo 备注,c.Name 门店,stuff(m.CardID,8,4,'****') 会员卡号,m.WeChatName 微信名 from dbo.TGiftOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            sql += where.Replace("*", "c");
         
            return await _comService.DataTableToExcel(sql);
        }
    }
}
