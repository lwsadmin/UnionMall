using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnionMall.Common
{
    public interface ICommonAppService : IApplicationService
    {
        JsonResult SaveSingleImg(IFormFile file, int tenandId);
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);

        string GetWhere();



    }
}
