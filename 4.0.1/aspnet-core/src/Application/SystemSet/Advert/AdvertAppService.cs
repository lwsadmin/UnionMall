using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.SystemSet
{
    public class AdvertAppService : ApplicationService,IAdvertAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Advert, long> _Repository;
        public readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        public AdvertAppService(ISqlExecuter sqlExecuter, IRepository<Advert, long> Repository, IHostingEnvironment HostingEnvironment,
            IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _HostingEnvironment = HostingEnvironment;
            _AbpSession = AbpSession;
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select a.id,a.tenantid,a.title,a.image,a.sort,a.linkurl from tadvert a where 1=1";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                table += $" and TenantId={_AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task CreateOrEditAsync(Advert dto)
        {
            if (dto.Id > 0)
            {
                await _Repository.UpdateAsync(dto);
            }
            else
            {
                if (_AbpSession.TenantId != null)
                { dto.TenantId = (int)_AbpSession.TenantId; }
                await _Repository.InsertAsync(dto);
            }
        }

        public async Task<Advert> GetByIdAsync(long Id)
        {
            var s = await _Repository.GetAsync(Id);
            return s;
        }

        public async Task DeleteAsync(long id)
        {
            var query = await _Repository.FirstOrDefaultAsync(c => c.Id == id);
            if (query != null && !string.IsNullOrEmpty(query.Image))
            {
                if (File.Exists(_HostingEnvironment.WebRootPath + query.Image))
                {
                    File.Delete(_HostingEnvironment.WebRootPath + query.Image);
                }
                query.Image = "";
                await _Repository.UpdateAsync(query);

            }
        }
    }
}
