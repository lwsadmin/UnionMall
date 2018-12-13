using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.SystemSet;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.MobileTerminal.Controllers
{
    [Area("MobileTerminal")]
    [AbpMvcAuthorize("MobileTerminalSet.AdvertSet")]
    public class AdvertController : UnionMallControllerBase
    {

        private readonly IAdvertAppService _AppService;
        private readonly IAbpSession _AbpSession;
        public AdvertController(IAdvertAppService AppService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "a.sort desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}