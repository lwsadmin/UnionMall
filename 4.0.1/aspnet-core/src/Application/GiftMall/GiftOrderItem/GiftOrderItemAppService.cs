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
        //private readonly IRepository<Entity.GiftOrder, long> _Repository;
        private readonly IRepository<Entity.GiftOrderItem, long> _Repository;
        private readonly IImageAppService _imgService;
        public GiftOrderItemAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
    IRepository<Entity.GiftOrderItem, long> Repository,
    IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            //_itemRepository = itemRepository;
            _imgService = imgService;
        }


        public async Task DeleteByOrderAsync(long orderId)
        {
            await _Repository.DeleteAsync(C => C.GiftOrderId == orderId);
        }
        public async Task<List<GiftOrderDetail>> GetOrderDetail(long orderId)
        {
            List<GiftOrderDetail> d = new List<GiftOrderDetail>();

            try
            {
                List<GiftOrderItem> itemList = _Repository.GetAllList(c => c.GiftOrderId == orderId);
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

        public async Task CreateAsync(GiftOrderItem item)
        {
        
            await _Repository.InsertAsync(item);
           // throw new NotImplementedException();
        }
    }
}
