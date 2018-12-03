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

namespace UnionMall.Gift
{
    public class GiftOrderItemAppService : ApplicationService, IGiftOrderItemAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.GiftOrder, long> _Repository;
        private readonly IRepository<Entity.GiftOrderItem, long> _itemRepository;
        private readonly IImageAppService _imgService;
        public GiftOrderItemAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
    IRepository<Entity.GiftOrder, long> Repository, IRepository<Entity.GiftOrderItem, long> itemRepository,
    IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _itemRepository = itemRepository;
            _imgService = imgService;
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
o.BillNumber,o.Status,o.OperateTime,o.Way,o.Memo,c.Name,m.CardID,m.WeChatName from dbo.TGiftOrder o left join dbo.TChainStore c on o.ChainStoreId=c.Id
left join dbo.TMember m on o.MemberId=m.Id  where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " o");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        Task<GiftOrderItem> IGiftOrderItemAppService.GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GiftOrderDetail>> GetOrderDetail(long orderId)
        {
            List<GiftOrderDetail> d = new List<GiftOrderDetail>();

            try
            {
                List<GiftOrderItem> itemList = _itemRepository.GetAllList(c => c.GiftOrderId == orderId);
                foreach (GiftOrderItem item in itemList)
                {
                    GiftOrderDetail o = new GiftOrderDetail();
                    o.Count = item.Count;
                    o.Point = item.Point;
                    o.GiftName = _sqlExecuter.
                        ExecuteDataSet($"select Name from tgift where id={item.GiftId}")
                        .Tables[0].Rows[0][0].ToString();
                    d.Add(o);
                }
                return d;
            }
            catch (Exception e)
            {

                throw new Exception(e.StackTrace + e.Message);
            }

        }
    }
}
