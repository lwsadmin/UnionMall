using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GiftMall.Controllers
{
    [Area("GiftMall")]
    [AbpMvcAuthorize("GiftMall.GiftCategory")]
    public class GiftCategoryController : UnionMallControllerBase
    {
        private readonly ICommonCategoryAppService _AppService;
        private readonly ICommonAppService _comService;
        public GiftCategoryController(ICommonCategoryAppService AppService, ICommonAppService comService)
        {
            _AppService = AppService;
            _comService = comService;
        }
        public IActionResult List(int pageIndex = 1, int pageSize = 10, string title = "")
        {
            string where = " and type= 1 " + _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and c.title like '%{title}%' ";
            int total;
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "c.sort desc, c.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}