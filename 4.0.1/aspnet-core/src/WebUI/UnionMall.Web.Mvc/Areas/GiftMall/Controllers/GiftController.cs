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
    [AbpMvcAuthorize("GiftMall.GiftCategory")]
    public class GiftController : UnionMallControllerBase
    {

        private readonly ICommonCategoryAppService _catAppService;
        private readonly IGiftAppService _AppService;
        private readonly ICommonAppService _comService;
        public GiftController(ICommonCategoryAppService catAppService, IGiftAppService AppService, ICommonAppService comService)
        {
            _catAppService = catAppService;
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string categoryId = "", string title = "")
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and g.name like '%{title}%' ";
            if (!string.IsNullOrEmpty(categoryId))
                where += $" and g.categoryId ={categoryId} ";
            int total;
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "g.sort desc,g.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            ViewBag.Category = await _catAppService.GetCategoryDropDownList(0, 1);
            return View(pageList);
        }
        public async Task<IActionResult> Add(long? id)
        {
            Entity.Gift a = new Entity.Gift();
            if (id != null)
            {
                a = await _AppService.GetByIdAsync((long)id);
            }
            var list = await _catAppService.GetCategoryDropDownList(0, 1);
            ViewData.Add("Category", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(list, "Id", "Title"));
            return View(a);
        }
    }
}