using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
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
        public readonly IAbpSession _AbpSession;
        public GoodsCategoryAppService(ISqlExecuter sqlExecuter,
            IRepository<model.GoodsCategory, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            DataSet ds = _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }

        public async Task CreateOrEditAsync(CategoryEditDto dto)
        {
            try
            {
                if (dto.Id > 0)
                {
                    await _Repository.UpdateAsync(dto.MapTo<model.GoodsCategory>());
                }
                else
                {
                    if (_AbpSession.TenantId != null)
                    { dto.TenantId = (int)_AbpSession.TenantId; }
                    await _Repository.InsertAsync(dto.MapTo<model.GoodsCategory>());
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }

        }

        public async Task<CategoryEditDto> GetByIdAsync(long id)
        {
            var s = await _Repository.GetAsync(id);
            return s.MapTo<CategoryEditDto>();
        }

        public List<DropDownDto> GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0)
        {
            DataSet ds = _sqlExecuter.GetCategoryDropDownList(tenantId, parentId, type);
            List<DropDownDto> dropDownList = new List<DropDownDto>();


            if (ds.Tables[0].Rows.Count == 0)
                return dropDownList;

            int maxLevel = Convert.ToInt32(ds.Tables[0].Select("1=1", " level desc")[0]["level"]);
            for (int i = 0; i <= maxLevel; i++)
            {
                foreach (DataRow item in ds.Tables[0].Select("level=" + i))
                {
                    DropDownDto dto = new DropDownDto();
                    dto.Id = (long)item["id"];
                    dto.Title = item["title"].ToString();
                    dto.ParentId = (long)item["ParentId"];
                    dto.Level = (int)item["Level"];
                    if (dto.Level != 0)
                        dto.Title = "├" + dto.Title;
                    for (int j = 0; j < dto.Level; i++)
                    {
                        dto.Title = System.Web.HttpUtility.HtmlDecode("&nbsp;") + dto.Title;
                    }
                    if (i == 0)
                        dropDownList.Add(dto);
                    else
                    {
                        int index = dropDownList.IndexOf(dropDownList.Find(c => c.Id == dto.ParentId));
                        dropDownList.Insert(index, dto);
                    }

                }
            }
            return dropDownList;
        }
        public async Task DeleteAsync(long id)
        {
            await _Repository.DeleteAsync(id);
            // throw new NotImplementedException();
        }

        public async Task<List<CategoryEditDto>> GetAllListByParentIdAsync(long parentId)
        {
            var query = await _Repository.GetAllListAsync(c => c.ParentId == parentId);
            return query.MapTo<List<CategoryEditDto>>(); ;
        }
    }
}
