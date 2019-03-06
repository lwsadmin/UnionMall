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
using UnionMall.FlashSale.Dto;
using UnionMall.Common;
using Abp.UI;
namespace UnionMall.FlashSale
{
    public class FlashSaleOrderItemAppService : ApplicationService, IFlashSaleOrderItemAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.FlashSaleOrderItem, long> _Repository;
        private readonly IImageAppService _imgService;
        public FlashSaleOrderItemAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
    IRepository<Entity.FlashSaleOrderItem, long> Repository,
    IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
        }
        public async Task CreateOrEditAsync(FlashSaleOrderItem model)
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
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.FlashSaleOrderItem> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select o.Id, convert(nvarchar(100),o.CreationTime,120) CreationTime,cast(o.Point as float) Point,
o.BillNumber,o.Status,o.OperateTime,o.Way,o.Memo,c.Name,m.CardID,m.WeChatName from dbo.TGiftOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " o");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }


       // [RemoteService(IsEnabled = false)]
        public async Task<List<OrderDetail>> GetOrderDetail(long orderId)
        {
            List<OrderDetail> d = new List<OrderDetail>();

            try
            {
                List<FlashSaleOrderItem> itemList = _Repository.GetAllList(c => c.OrderId == orderId);
                foreach (FlashSaleOrderItem item in itemList)
                {
                    OrderDetail o = new OrderDetail();
                    o.Count = item.Count;
                    o.Price = item.Price;
                    o.GoodsName = _sqlExecuter.
                        ExecuteDataSet($"select Title from tflashsale where id={item.FlashSaleId}")
                        .Tables[0].Rows[0][0].ToString();
                    d.Add(o);
                }
                return d;
            }
            catch (Exception e)
            {

                throw new UserFriendlyException(e.StackTrace + e.Message);
            }

        }
    }
}
