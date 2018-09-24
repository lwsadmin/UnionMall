using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UnionMall.EntityFrameworkCore;
using UnionMall.Goods.GoodsCategory.Dto;
using UnionMall.IRepositorySql;
using model = UnionMall.Goods.Category;
namespace UnionMall.Goods.GoodsCategory
{
    public class GoodsCategoryAppService : ApplicationService, IGoodsCategoryAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<model.GoodsCategory, long> _Repository;
        public GoodsCategoryAppService(ISqlExecuter sqlExecuter, IRepository<model.GoodsCategory, long> Repository)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            DataSet ds = _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }

        public async Task CreateOrEditAsync(CategoryEditDto dto)
        {
            if (dto.Id > 0)
                await _Repository.UpdateAsync(dto.MapTo<model.GoodsCategory>());
            else
                await _Repository.InsertAsync(dto.MapTo<model.GoodsCategory>());

        }

        public async Task<CategoryEditDto> GetByIdAsync(long id)
        {
            var s = await _Repository.GetAsync(id);
            return s.MapTo<CategoryEditDto>();
        }

        public DataSet GetCategoryDropDownList(int? tenantId, int parentId = 0, int type = 0)
        {
            DataSet ds = _sqlExecuter.GetCategoryDropDownList(tenantId, parentId, type);
            return ds;
        }
    }
}
