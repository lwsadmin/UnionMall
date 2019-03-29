using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using UnionMall.Common.Dto;

=======
>>>>>>> parent of c5fd376... 1
using UnionMall.Entity;
namespace UnionMall.Common.CommonSpec
{
    public interface ICommonSpecAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
<<<<<<< HEAD
        Task CreateOrEditAsync(CreateOrEdit cat);
        Task<CreateOrEdit> GetByIdAsync(Guid Id);
        Task Delete(Guid id);
        Task<List<SpecDropDown>> GetDropDown();
=======
        Task CreateOrEditAsync(Entity.CommonSpec cat);
        Task<Entity.CommonSpec> GetByIdAsync(long Id);
        Task Delete(long id);
>>>>>>> parent of c5fd376... 1
        Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0);
    }
}
