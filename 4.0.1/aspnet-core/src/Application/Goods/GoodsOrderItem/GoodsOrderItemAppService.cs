using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common;
using UnionMall.Entity;
using UnionMall.Goods.Dto;
using UnionMall.IRepositorySql;

namespace UnionMall.Goods
{
    public class GoodsOrderItemAppService : ApplicationService, IGoodsOrderItemAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.GoodsOrderItem, long> _Repository;
        private readonly IImageAppService _imgService;
        private readonly ICommonAppService _comService;

        public GoodsOrderItemAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
            ICommonAppService comService,
    IRepository<Entity.GoodsOrderItem, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
            _comService = comService;
        }
        public async Task CreateOrEditAsync(Entity.GoodsOrderItem model)
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



        public async Task<Entity.GoodsOrderItem> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<List<GoodsOrderDetail>> GetOrderDetail(long orderId)
        {
            List<GoodsOrderDetail> list = new List<GoodsOrderDetail>();

            try
            {
                string sql = $@"select g.Name,i.Count,cast(i.Price as float) Price,
stuff((select ','+isnull([Text],'') from dbo.TCommonSpecValue  
where charindex(cast(id as nvarchar(200)),b.ValueText)>0 for xml path('')),1,1,'') Spec
 from dbo.TGoodsOrderItem i 
left join dbo.TGoods g on i.GoodsId=g.Id 
left join dbo.TCommonSpecObject b on i.SpecObjectId=b.id
where goodsorderid={orderId}";
                DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    GoodsOrderDetail d = new GoodsOrderDetail();
                    d.Count = item["Count"].ToString();
                    d.Price= item["Price"].ToString();
                    d.GoodsSpec= item["Spec"].ToString();
                    d.GoodsName= item["Name"].ToString();
                    list.Add(d);
                }
                return list;
            }
            catch (Exception e)
            {

                throw new UserFriendlyException(e.StackTrace + e.Message);
            }
        }
    }
}
