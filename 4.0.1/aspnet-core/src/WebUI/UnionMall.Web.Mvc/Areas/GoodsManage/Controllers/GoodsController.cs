using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Business;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Goods;
using UnionMall.Goods.Dto;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsManage")]
    public class GoodsController : UnionMallControllerBase
    {
        private readonly IGoodsAppService _AppService;
        private readonly IAbpSession _AbpSession;
        private readonly ICommonAppService _comService;
        private readonly IGoodsCategoryAppService _catAppService;
        private readonly IBrandAppService _brandAppService;
        private readonly IChainStoreAppService _storeAppService;
        public GoodsController(IGoodsAppService AppService,
            ICommonAppService comService, IAbpSession abpSession,
            IGoodsCategoryAppService catAppService, IBrandAppService brandAppService,
            IChainStoreAppService storeAppService)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
            _comService = comService;
            _catAppService = catAppService;
            _brandAppService = brandAppService;
            _storeAppService = storeAppService;
        }
        public async Task<IActionResult> List(string goodsName, string businessId, string categoryId,
          string brandId, string type, string status, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(goodsName))
            { where = $" and g.Name like '%{goodsName}%'"; }
            if (!string.IsNullOrEmpty(categoryId))
            { where += $" and g.categoryId = {categoryId}"; }
            if (!string.IsNullOrEmpty(brandId))
            { where += $" and g.brandId = {brandId}"; }
            if (!string.IsNullOrEmpty(businessId))
            { where += $" and g.BusinessId = {businessId}"; }
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
            List<DropDownDto> dtoList = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("cat", new SelectList(dtoList, "Id", "Title"));

            List<BrandSelectDto> t = _brandAppService.GetMultiSelect();
            ViewData.Add("brand", t);

            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));
            return View(pageList);
        }
    }
}