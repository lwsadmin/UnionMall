using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;

namespace UnionMall.Article
{
    public class ArticleAppService : ApplicationService, IArticleAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.Article, long> _Repository;

        public ArticleAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.Article, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public async Task DeleteAsync(long id)
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

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select a.id,a.title,a.sort,a.CreationTime,a.Source,a.Author,a.Status, a.smallimg,
c.Title CTitle,
a.click,c.title,u.UserName from TArticle a left join TCommonCategory c on a.categoryId=c.Id
left join TUsers u on a.userid=u.id left join TChainStore s on u.ChainStoreId=s.Id
where 1=1";
            }
            where = where.Replace("*.BusinessId", "s.BusinessId").Replace(" *", " a");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        public async Task<Entity.Article> GetByIdAsync(long id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task CreateOrEditAsync(Entity.Article model)
        {
            model.TenantId = _AbpSession.TenantId ?? 0;
            if (model.Id <= 0)
            {
                model.UserId = _AbpSession.UserId ?? 0;
                await _Repository.InsertAsync(model);
            }
            else
            {
                await _Repository.UpdateAsync(model);
            }
        }
    }
}
