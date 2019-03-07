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
using UnionMall.Goods.Dto;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
namespace UnionMall.Goods
{
    public class GoodsCategoryAppService : ApplicationService, IGoodsCategoryAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<GoodsCategory, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public GoodsCategoryAppService(ISqlExecuter sqlExecuter,
            IRepository<GoodsCategory, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            DataSet ds = _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }

        public async Task CreateOrEditAsync(CategoryEditDto dto)
        {
            try
            {
                if (dto.Id > 0)
                {
                    await _Repository.UpdateAsync(dto.MapTo<GoodsCategory>());
                }
                else
                {
                    if (_AbpSession.TenantId != null)
                    { dto.TenantId = (int)_AbpSession.TenantId; }
                    await _Repository.InsertAsync(dto.MapTo<GoodsCategory>());
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }

        }
        [RemoteService(IsEnabled = false)]
        public async Task<CategoryEditDto> GetByIdAsync(long id)
        {
            var s = await _Repository.GetAsync(id);
            return s.MapTo<CategoryEditDto>();
        }
        [RemoteService(IsEnabled = false)]
        public List<DropDownDto> GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0)
        {
            DataSet ds = _sqlExecuter.GetCategoryDropDownList(tenantId, parentId, type);
            List<DropDownDto> dropDownList = new List<DropDownDto>();


            if (ds.Tables[0].Rows.Count == 0)
                return dropDownList;

            int maxLevel = Convert.ToInt32(ds.Tables[0].Select("1=1", " level desc")[0]["level"]);
            for (int i = 0; i <= maxLevel; i++)
            {
                DataRow[] rows = ds.Tables[0].Select("level=" + i);
                if (rows.Length == 0)
                    break;
                foreach (DataRow item in rows)
                {
                    DropDownDto dto = new DropDownDto();
                    dto.Id = (long)item["id"];
                    dto.Title = item["title"].ToString();
                    dto.ParentId = (long)item["ParentId"];
                    dto.Level = (int)item["Level"];
                    if (dto.Level != 0)
                        dto.Title = "├" + dto.Title;
                    for (int j = 0; j < dto.Level; j++)
                    {
                        dto.Title = System.Web.HttpUtility.HtmlDecode("&nbsp;") + dto.Title;
                    }
                    if (i == 0)
                        dropDownList.Add(dto);
                    else
                    {
                        int index = dropDownList.IndexOf(dropDownList.Find(c => c.Id == dto.ParentId));
                        // dropDownList.in
                        dropDownList.Insert(index + 1, dto);
                    }

                }
            }

            return dropDownList;
        }
        [RemoteService(IsEnabled = false)]
        public bool Delete(long id, out string msg)
        {
            //  await _Repository.DeleteAsync(id);
            // throw new NotImplementedException();
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            var count = _Repository.GetAllList(c => c.ParentId == id).Count;
            if (count > 0)
            {
                msg = "HasChild";
                return false;
            }
            _Repository.Delete(id);
            msg = "";
            return true;
        }
        [RemoteService(IsEnabled = false)]
        public async Task<List<CategoryEditDto>> GetAllListByParentIdAsync(long parentId)
        {
            var query = await _Repository.GetAllListAsync(c => c.ParentId == parentId);
            return query.MapTo<List<CategoryEditDto>>();
        }
        [RemoteService(IsEnabled = false)]
        public async Task<DataTable> GetGoodsCategory()
        {
            string sql = $@"select c.id, c.Title,count(g.Id) from TGoodsCategory c 
left join TGoods  g on g.CategoryId=c.Id  where c.TenantId={AbpSession.TenantId}
group by c.Id,c.Title having count(g.Id)>0";

            DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
            return dt;
            //throw new NotImplementedException();
        }
    }
}
