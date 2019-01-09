using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;
using UnionMall.Entity;
using UnionMall.Goods.Dto;
using System.Linq.Expressions;
namespace UnionMall.Goods
{
    public class BrandAppService : ApplicationService, IBrandAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Brand, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public readonly IGoodsCategoryAppService _catService;
        public BrandAppService(ISqlExecuter sqlExecuter,
            IRepository<Brand, long> Repository, IAbpSession AbpSession, IGoodsCategoryAppService catService)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _catService = catService;
        }
        public async Task<JsonResult> CreateOrEditAsync(Brand dto)
        {
            if (dto.Id > 0)
            {
                await _Repository.UpdateAsync(dto);
                return new JsonResult(new { succ = true, msg = "" });
            }
            else
            {
                //var query = _Repository.FirstOrDefault(c => c.Title == dto.Title);
                //if (query != null)
                //{
                //    return new JsonResult(new { succ = false, msg = "{0}AlreadyExist" });
                //}


                if (_AbpSession.TenantId != null)
                { dto.TenantId = (int)_AbpSession.TenantId; }
                await _Repository.InsertAsync(dto);
                return new JsonResult(new { succ = true, msg = "" });
            }

        }

        public async Task<JsonResult> DeleteAsync(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query == null)
            {
                return new JsonResult(new { succ = true, msg = "NotExist" });
            }
            await _Repository.DeleteAsync(id);
            return new JsonResult(new { succ = true, msg = "" });
        }

        public async Task<Brand> GetByIdAsync(long Id)
        {

            var s = await _Repository.GetAsync(Id);
            return s;
        }

        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            DataSet ds = _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }
        public List<BrandSelectDto> GetMultiSelect(long catId = 0)
        {

            string sql = $@"select id,Title from dbo.TBrand where 1=1";
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0 && catId == 0)
            {
                sql += $@" and TenantId={_AbpSession.TenantId}";
            }
            if (catId > 0)
            {
                var cat = _catService.GetByIdAsync(catId).Result;
                if (!string.IsNullOrEmpty(cat.Brand))
                {
                    string s = cat.Brand.Remove(cat.Brand.LastIndexOf(","));
                    sql += $@" and id in ({s})";
                }
                else
                {
                    return new List<BrandSelectDto>();
                }
            }
            List<BrandSelectDto> list = new List<BrandSelectDto>();
            DataTable t = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
            foreach (DataRow item in t.Rows)
            {
                BrandSelectDto dto = new BrandSelectDto();
                dto.Id = (long)item["id"];
                dto.Title =item["title"].ToString();
                list.Add(dto);
            }
            return list;
        }
    }
}
