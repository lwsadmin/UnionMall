using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Gift;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GiftMall.Controllers
{
    [Area("GiftMall")]
    [AbpMvcAuthorize("GiftMall.GiftOrder")]
    public class GiftOrderController : UnionMallControllerBase
    {
        private readonly ICommonCategoryAppService _catAppService;
        private readonly IImageAppService _imgAppService;
        private readonly IGiftOrderAppService _AppService;
        private readonly ICommonAppService _comService;
        public GiftOrderController(ICommonCategoryAppService catAppService,
            ICommonAppService comService, IGiftOrderAppService AppService, IImageAppService imgAppService)
        {
            _catAppService = catAppService;
            _comService = comService;
            _AppService = AppService;
            _imgAppService = imgAppService;
        }

        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string categoryId = "", string title = "")
        {
            //string where = _comService.GetWhere();
            //if (!string.IsNullOrEmpty(title))
            //    where += $" and g.name like '%{title}%' ";
            //if (!string.IsNullOrEmpty(categoryId))
            //    where += $" and g.categoryId ={categoryId} ";
            //int total;

            //DataSet ds = _AppService.GetPage(pageIndex, pageSize, "g.sort desc,g.id desc", out total, where);
            //IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            //ViewBag.PageSize = pageSize;
            //if (Request.Headers.ContainsKey("x-requested-with"))
            //{
            //    return View("_Table", pageList);
            //}
            //ViewBag.Category = await _catAppService.GetCategoryDropDownList(0, 1);
            return View();
        }
    }
}