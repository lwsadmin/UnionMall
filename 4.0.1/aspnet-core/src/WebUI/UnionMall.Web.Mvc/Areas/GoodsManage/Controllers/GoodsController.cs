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
using UnionMall.Entity;
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
        private readonly IImageAppService _imgAppService;
        public GoodsController(IGoodsAppService AppService,
            ICommonAppService comService, IAbpSession abpSession,
            IGoodsCategoryAppService catAppService, IBrandAppService brandAppService, IImageAppService imgAppService,
            IChainStoreAppService storeAppService)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
            _comService = comService;
            _catAppService = catAppService;
            _brandAppService = brandAppService;
            _storeAppService = storeAppService;
            _imgAppService = imgAppService;
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

        public async Task<IActionResult> Add(long? id)
        {
            CreateOrEditDto s = new CreateOrEditDto();
            List<Entity.Image> imageList = new List<Entity.Image>();
            Entity.Goods a = new Entity.Goods();
            if (id != null)
            {
                a = await _AppService.GetByIdAsync((long)id);
                imageList = await _imgAppService.GetList(c => c.ObjectId == id && c.Type == (int)ImageType.商品图片);
                string imgs = "", config = "";
                for (int i = 0; i < imageList.Count; i++)
                {
                    imgs += $"\'{imageList[i].Url.ToString()}\',";
                    //config += string.Format("{" + "key: 'item{0}',url:'{1}',size:{2}" + "}",
                    //    imageList[i].ToString(), imageList[i].Url, imageList[i].Size);
                    config += "{img:'" + imageList[i].Url + "', key: '" + imageList[i].Id.ToString() + "',url:'/common/deleteimg'},";
                }
                ViewBag.Images = imgs;
                ViewBag.Config = config;
            }
            s.Goods = a;
            s.ImageList = imageList;

            var cat = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("Category", new SelectList(cat, "Id", "Title"));
            var b = _brandAppService.GetMultiSelect();
            ViewData.Add("Brand", new SelectList(b, "Id", "Title"));

            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));

            IList<SelectListItem> listItem = new List<SelectListItem>();
            Array values = System.Enum.GetValues(typeof(Entity.GoodsType));
            foreach (int item in values)
            {
                listItem.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = L(System.Enum.GetName(typeof(Entity.GoodsType), item))
                });
            }
            ViewData.Add("GoodsType", new SelectList(listItem, "Value", "Text"));
            return View(s);
        }
    }
}