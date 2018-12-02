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
    public class GiftOrderAppService : ApplicationService, IGiftOrderAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.Gift, long> _Repository;
        private readonly IImageAppService _imgService;
        public GiftOrderAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
    IRepository<Entity.Gift, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
        }
        public async Task CreateOrEditAsync(CreateOrEditDto model)
        {
            if (model.Gift.Id >= 0)
            {
                await _Repository.UpdateAsync(model.Gift);

            }
            else
            {
                await _Repository.InsertAsync(model.Gift);
                for (int i = 0; i < model.ImageList.Count; i++)
                {
                    model.ImageList[i].ObjectId = model.Id;
                }
            }
            await _imgService.CreateOrEditAsync(model.ImageList);
        }

        public async Task DeleteAsync(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<Entity.Gift> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select g.id,g.name,c.Title,b.BusinessName, g.Img,g.Sort,cast(g.Integral as float) Integral,
g.SingleReceiveCount,g.StockCount,g.Status from dbo.TGift g left join dbo.TCommonCategory c on g.categoryid=c.id
left join dbo.TBusiness b on g.businessid=b.id
where 1=1";
            }
            where = where.Replace("*.BusinessId", "g.BusinessId").Replace(" *", " g");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
