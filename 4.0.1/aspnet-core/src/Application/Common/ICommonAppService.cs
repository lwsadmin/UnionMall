using System;
using System.Collections.Generic;
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
    }
}
