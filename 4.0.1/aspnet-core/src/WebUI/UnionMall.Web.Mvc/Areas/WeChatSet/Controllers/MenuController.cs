using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Mvc.Areas.WeChatSet.Controllers
{
    [Area("WeChatSet")]
    [AbpMvcAuthorize("WeChatSet.WeChatDocking")]
    public class MenuController : UnionMallControllerBase
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}