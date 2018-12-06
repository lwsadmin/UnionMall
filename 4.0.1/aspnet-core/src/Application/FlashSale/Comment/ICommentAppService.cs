using Abp.Application.Services;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnionMall.Article.Dto;

namespace UnionMall.FlashSale
{
    public interface ICommentAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(Entity.Comment model);
        Task<Entity.Comment> GetByIdAsync(long Id);
        Task DeleteAsync(long id);
        Task<List<ReplyListDto>> GetComByParentIdAsync(long parntId);
    }
}
