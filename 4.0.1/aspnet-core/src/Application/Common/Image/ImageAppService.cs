using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
using UnionMall.IRepositorySql;
using System.IO;
namespace UnionMall.Common
{
    public class ImageAppService : ApplicationService, IImageAppService
    {
        private readonly IRepository<Image, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        public ImageAppService(IRepository<Image, long> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _sqlExecuter = sqlExecuter;
        }
        public async Task<List<Image>> GetList(Expression<Func<Image, bool>> linq)
        {
            return await _Repository.GetAllListAsync(linq);
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select i.id,i.Url,i.Size,i.Name,i.BusinessId,c.Name StoreName from TImage i left join TChainStore c
on i.ChainStoreId =c.id where 1=1 ";
            }
            where = where.Replace(" *", " i");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        public async Task CreateOrEditAsync(Image img)
        {
            if (img.Id <= 0)
            {
                await _Repository.InsertAsync(img);
            }
            else
            {
                await _Repository.UpdateAsync(img);
            }
        }
        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                if (File.Exists(_HostingEnvironment.WebRootPath + query.Url))
                {
                    File.Delete(_HostingEnvironment.WebRootPath + query.Url);
                }
                await _Repository.DeleteAsync(query);

            }
        }
        public async Task DeleteOnlyImg(string url)
        {
            if (File.Exists(_HostingEnvironment.WebRootPath + url))
            {
                File.Delete(_HostingEnvironment.WebRootPath + url);
            }
        }

        public async Task CreateOrEditAsync(List<Image> imgList)
        {
            foreach (Image item in imgList)
            {
                if (item.Id <= 0)
                {
                    await _Repository.InsertAsync(item);
                }
                else
                {
                    await _Repository.UpdateAsync(item);
                }
            }
        }
    }
}
