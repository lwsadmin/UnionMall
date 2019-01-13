using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Mvc.Areas.OffLine.Controllers
{
    [Area("OffLine")]
    [AbpMvcAuthorize("OffLineCashie.QuickConsume")]
    public class QuickConsumeController : UnionMallControllerBase
    {
        public QuickConsumeController()
        {

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}