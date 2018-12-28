using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;
using UnionMall.Common.Attribute;
using UnionMall.Controllers;
using UnionMall.Goods;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsManage")]
    public class GoodsAttrController : UnionMallControllerBase
    {
        private readonly ICommonAttributeAppService _AppService;
        private readonly IGoodsCategoryAppService _catAppService;
        public GoodsAttrController(ICommonAttributeAppService AppService,
            IGoodsCategoryAppService catAppService )
        {
            _AppService = AppService;
            _catAppService = catAppService;
        }
        public async Task<IActionResult> List(string name,string categoryId, int page = 1, int pageSize = 10)
        {
            string where =string.Empty;
            if (!string.IsNullOrEmpty(name))
            { where = $" and a.Name like '%{name}%'"; }
            if (!string.IsNullOrEmpty(categoryId))
            { where = $" and a.categoryId = '{categoryId}'"; }
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, " ", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            var cat = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("Category", new SelectList(cat, "Id", "Title"));
            return View(pageList);
        }
    }
}