using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
namespace UnionMall.Common
{
    public interface IImageAppService : IApplicationService
    {
        //JsonResult SaveSingleImg(IFormFile file, int tenandId);
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task<List<Entity.Image>> GetList(Expression<Func<Image, bool>> linq);
        Task CreateOrEditAsync(Image img);
        Task CreateOrEditAsync(List<Image> imgList);
        Task Delete(long id);
        Task DeleteOnlyImg(string url);
    }
}
