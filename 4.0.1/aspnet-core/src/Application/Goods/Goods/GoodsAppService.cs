using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common;
using UnionMall.Common.CommonSpec;
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
        private readonly ISpecObjectAppService _specObjService;

        public GoodsAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.Goods, long> Repository,
            IAbpSession AbpSession, IImageAppService imgService, ISpecObjectAppService specObjService)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
            _specObjService = specObjService;
        }
        public async Task CreateOrEditAsync(CreateOrEditDto model)
        {
            if (model.Goods.Id > 0)
            {
                await _Repository.UpdateAsync(model.Goods);
                if (model.ValueList.Count > 0)
                {
                    await _specObjService.Delete(c => c.ObjectId == model.Goods.Id);
                }
            }
            else
            {
                await _Repository.InsertAsync(model.Goods);
                for (int i = 0; i < model.ImageList.Count; i++)
                {
                    model.ImageList[i].ObjectId = model.Goods.Id;
                }

            }

            for (int i = 0; i < model.ValueList.Count; i++)
            {
                model.ValueList[i].ObjectId = model.Goods.Id;
                model.ValueList[i].TenantId = model.Goods.TenantId;
                model.ValueList[i].Type = 0;
                await _specObjService.AddOrEdit(model.ValueList[i]);
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
b.Title BTitle,s.Name StoreName,cast(g.Price as float) Price ,cast(g.RetailPrice as float) RetailPrice from dbo.TGoods g left join dbo.TGoodsCategory c
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
