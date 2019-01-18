using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Common.CommonSpec;
using UnionMall.Controllers;
using UnionMall.Goods;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.OffLine.Controllers
{
    [Area("OffLine")]
    [AbpMvcAuthorize("OffLineCashie.GoodsConsume")]
    public class GoodsConsumeController : UnionMallControllerBase
    {
        private readonly IGoodsCategoryAppService _catAppService;
        private readonly IGoodsAppService _AppService;
        private readonly ICommonAppService _comService;
        //private readonly ICommonSpecAppService _specAppService;

        private readonly ISpecObjectAppService _specAppService;
        public GoodsConsumeController(IGoodsCategoryAppService catAppService,
            IGoodsAppService AppService, ICommonAppService comService, ISpecObjectAppService specAppService)
        {

            _catAppService = catAppService;
            _AppService = AppService;
            _comService = comService;
            _specAppService = specAppService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10, long categoryId = 0, string title = "")
        {
            string where = _comService.GetWhere();
            ViewBag.Title = title;
            ViewBag.Cat = categoryId;
            if (!string.IsNullOrEmpty(title))
                where += $" and g.name like '%{title}%' ";
            if (categoryId > 0)
                where += $" and g.categoryId ={categoryId} ";
            int total;
            string tableSl = $@"select g.id,g.Status,g.Sort,g.Stock,g.Name,
s.Name StoreName,cast(g.Price as float) Price ,
cast(g.RetailPrice as float) RetailPrice from dbo.TGoods g
left join dbo.TChainStore s on g.chainstoreid=s.Id";
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "g.sort desc", out total, where, tableSl);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            ViewBag.Cat = categoryId;
            ViewBag.Category = (await _catAppService.GetGoodsCategory()).Rows;
            return View(pageList);
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, long categoryId = 0, string title = "")
        {
            string where = _comService.GetWhere();
            ViewBag.Title = title;
            ViewBag.Cat = categoryId;
            if (!string.IsNullOrEmpty(title))
                where += $" and g.name like '%{title}%' ";
            if (categoryId > 0)
                where += $" and g.categoryId ={categoryId} ";
            int total;
            string tableSl = $@"select g.id,g.Status,g.Sort,g.Stock,g.Name,
s.Name StoreName,cast(g.Price as float) Price ,
cast(g.RetailPrice as float) RetailPrice from dbo.TGoods g
left join dbo.TChainStore s on g.chainstoreid=s.Id where 1=1";
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "g.sort desc", out total, where, tableSl);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            return PartialView("_Table", pageList);
        }

        public async Task<ActionResult> Add(long goodsId)
        {
            DataTable dt = await _specAppService.GetObjTableBuyObjId(goodsId);
            return View("_Select", dt);
        }
    }
}