﻿using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Cateogory.Dto;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.Cateogory
{
    public class CommonCategoryAppService : ApplicationService, ICommonCategoryAppService
    {

        private readonly IRepository<CommonCategory, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonCategoryAppService(IRepository<CommonCategory, long> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _sqlExecuter = sqlExecuter;
        }
        public async Task CreateOrEditAsync(CommonCategory cat)
        {
            DataTable dt = _sqlExecuter.ExecuteDataSet($@"select c.BusinessId  from dbo.TUsers s 
left join TChainStore c on s.ChainStoreId = c.Id where s.id={_AbpSession.UserId}").Tables[0];
            cat.TenantId = _AbpSession.TenantId ?? 0;
            cat.BusinessId = (long)dt.Rows[0][0];
            if (cat.Id <= 0)
            {
                await _Repository.InsertAsync(cat);
            }
            else
            {
                await _Repository.UpdateAsync(cat);
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                var query = _Repository.FirstOrDefault(c => c.Id == id);
                if (query != null)
                {
                    await _Repository.DeleteAsync(query);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace + e.Message);
                throw;
            }


        }
        [RemoteService(IsEnabled = false)]
        public Task<List<CommonCategory>> GetAllListByParentIdAsync(long parentId)
        {
            throw new NotImplementedException();
        }
        [RemoteService(IsEnabled = false)]
        public Task<CommonCategory> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }
        [RemoteService(IsEnabled = false)]
        public async Task<List<CatDropDownDto>> GetCategoryDropDownList(long parentId = 0, int type = 0)
        {
            var query = await _Repository.GetAllListAsync(c => c.Type == type && c.ParentId == parentId);
            if (_AbpSession.TenantId != null || (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }

            return query.MapTo<List<CatDropDownDto>>();
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.id, c.title,c.id,c.Memo,c.sort,b.BusinessName from TCommonCategory c left join TBusiness b on c.businessid=b.id
where 1=1 ";
            }
            where = where.Replace(" *", " c");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
