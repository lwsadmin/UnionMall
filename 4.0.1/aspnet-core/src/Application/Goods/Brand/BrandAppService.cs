using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;

namespace UnionMall.Goods.Brand
{
    public class BrandAppService : ApplicationService, IBrandAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Brand, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public BrandAppService(ISqlExecuter sqlExecuter,
            IRepository<Brand, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
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
            DataSet ds = _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }
    }
}
