using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.SystemSet;
namespace UnionMall.Web.Mvc.Areas.WeChatSet.Controllers
{
    [Area("WeChatSet")]
    [AbpMvcAuthorize("WeChatSet.WeChatDocking")]
    public class MenuController : UnionMallControllerBase
    {
        private readonly IMenuAppService _appService;
        public MenuController(IMenuAppService appService)
        {
            _appService = appService;
        }
        public async Task<IActionResult> Index()
        {
            Menu m = new Menu();
            return View(m);
        }
    }
}