using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common;
using UnionMall.Entity;
using UnionMall.Goods.Dto;
using UnionMall.IRepositorySql;

namespace UnionMall.Goods
{
    public class GoodsAppService : ApplicationService, IGoodsAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Entity.Goods, long> _Repository;
        public readonly IAbpSession _AbpSession;
        private readonly IImageAppService _imgService;
        public GoodsAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.Goods, long> Repository, 
            IAbpSession AbpSession, IImageAppService imgService)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
        }
        public async Task CreateOrEditAsync(CreateOrEditDto model)
        {
            if (model.Goods.Id > 0)
            {
                await _Repository.UpdateAsync(model.Goods);
            }
            else
            {
                await _Repository.InsertAsync(model.Goods);
                for (int i = 0; i < model.ImageList.Count; i++)
                {
                    model.ImageList[i].ObjectId = model.Id;
                }
            }
            await _imgService.CreateOrEditAsync(model.ImageList);
        }

        public async Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Entity.Goods> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select g.id,g.Image,g.Status,g.Sort,g.Click,g.SellCount,g.Name,c.Title,
b.Title BTitle,s.Name StoreName, g.Price,g.RetailPrice from dbo.TGoods g left join dbo.TGoodsCategory c
on g.CategoryId=c.Id left join dbo.TBrand b on g.BrandId=b.Id
left join dbo.TChainStore s on g.chainstoreid=s.Id
where 1=1";
            }
            where = where.Replace("*.BusinessId", "s.BusinessId").Replace(" *", " g");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
