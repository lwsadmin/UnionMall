using Abp.Application.Services;
using System.Data;
using System.Threading.Tasks;

namespace UnionMall.Article
{
    public interface IArticleAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.Article model);
        Task<Entity.Article> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
    }
}
