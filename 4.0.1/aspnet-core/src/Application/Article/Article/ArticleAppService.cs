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
        public async Task Delete(long id)
        {
            await _Repository.DeleteAsync(c => c.Id == id);
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select a.id,a.title,a.sort,a.CreationTime,a.smallimg,a.click,c.title,u.UserName 
 from TArticle a left join TCommonCategory c on a.categoryId=c.Id
left join TUsers u on a.userid=u.id left join TChainStore s on u.ChainStoreId=s.Id
where 1=1";
            }
            where = where.Replace(" *", "a");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
