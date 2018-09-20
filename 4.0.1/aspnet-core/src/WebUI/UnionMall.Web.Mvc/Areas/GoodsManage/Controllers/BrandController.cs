using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [AbpMvcAuthorize]
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}