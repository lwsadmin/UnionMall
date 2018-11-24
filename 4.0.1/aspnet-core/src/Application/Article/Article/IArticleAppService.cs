using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Article
{
    public interface IArticleAppService: IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        //Task CreateOrEditAsync(CreateEditDto dt);
        //Task<CreateEditDto> GetByIdAsync(long Id);

        Task Delete(long id);
    }
}
