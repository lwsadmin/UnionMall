using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Goods;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsManage")]
    public class GoodsAttrController : UnionMallControllerBase
    {
        private readonly IAbpSession _AbpSession;
        private readonly ICommonAppService _comService;
        private readonly IGoodsCategoryAppService _catAppService;
        public GoodsAttrController(IGoodsAppService AppService,
            ICommonAppService comService, IAbpSession abpSession,
            IGoodsCategoryAppService catAppService, IBrandAppService brandAppService, IImageAppService imgAppService,
            IChainStoreAppService storeAppService)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
            _comService = comService;
            _catAppService = catAppService;
        }
        public async Task<IActionResult> List(string goodsName, string storeId, string categoryId,
          string brandId, string type, string status, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(goodsName))
            { where = $" and g.Name like '%{goodsName}%'"; }
            if (!string.IsNullOrEmpty(categoryId))
            { where += $" and g.categoryId = {categoryId}"; }
            if (!string.IsNullOrEmpty(brandId))
            { where += $" and g.brandId = {brandId}"; }
            if (!string.IsNullOrEmpty(storeId))
            { where += $" and g.ChinStoreId = {storeId}"; }
            if (!string.IsNullOrEmpty(type))
            { where += $" and g.type = {type}"; }
            if (!string.IsNullOrEmpty(status))
            { where += $" and g.status = {status}"; }

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "g.sort desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }

            ViewBag.Category = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewBag.Brand = _brandAppService.GetMultiSelect();
            ViewBag.Store = (await _storeAppService.GetDropDown());

            return View(pageList);
        }
    }
}