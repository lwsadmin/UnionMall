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

namespace UnionMall.Goods
{
    public class GoodsAppService : ApplicationService, IGoodsAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Brand, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public GoodsAppService(ISqlExecuter sqlExecuter,
            IRepository<Brand, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public Task CreateOrEditAsync(Entity.Goods model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Entity.Goods> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select g.id,g.Image,g.Status,g.Sort,g.Click,g.SellCount,g.Name,c.Title,
b.Title BTitle,s.Name, g.Price,g.RetailPrice from dbo.TGoods g left join dbo.TGoodsCategory c
on g.CategoryId=c.Id left join dbo.TBrand b on g.BrandId=b.Id
left join dbo.TChainStore s on g.chainstoreid=c.Id
where 1=1";
            }
            where = where.Replace("*.BusinessId", "c.BusinessId").Replace(" *", " g");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
