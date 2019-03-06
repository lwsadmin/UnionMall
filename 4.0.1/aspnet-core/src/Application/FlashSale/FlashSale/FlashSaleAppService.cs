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

namespace UnionMall.FlashSale
{
    public class FlashSaleAppService : ApplicationService, IFlashSaleAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.FlashSale, long> _Repository;
        private readonly IImageAppService _imgService;
        public FlashSaleAppService(ISqlExecuter sqlExecuter, IImageAppService imgService,
    IRepository<Entity.FlashSale, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _imgService = imgService;
        }
        public async Task CreateOrEditAsync(CreateOrEditDto model)
        {
            if (model.FlashSale.Id >= 0)
            {
                await _Repository.UpdateAsync(model.FlashSale);

            }
            else
            {
                await _Repository.InsertAsync(model.FlashSale);
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
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.FlashSale> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select f.id,f.status, f.title,f.img,cast( f.price as float) price,
cast( f.marketprice as float) marketprice,f.sellcount,f.totalcount,f.img,f.sort,f.singlelimit,b.BusinessName from tflashsale f  left join dbo.TBusiness b
on f.businessid=b.id
where 1=1";
            }
            where = where.Replace(" *", " f");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
