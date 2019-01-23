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
using UnionMall.Coupon.ReceiveStatistics;
using UnionMall.Goods;
using UnionMall.Member;
using UnionMall.SystemSet;
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
        private readonly IReceiveStatisticsAppService _copAppService;
        private readonly IMemberAppService _memAppService;
        private readonly ISpecObjectAppService _specAppService;
        private readonly IParameterAppService _parService;
        public GoodsConsumeController(IGoodsCategoryAppService catAppService,
            IGoodsAppService AppService, ICommonAppService comService, IReceiveStatisticsAppService copAppService,
            ISpecObjectAppService specAppService, IMemberAppService memAppService, IParameterAppService parService)
        {
            _catAppService = catAppService;
            _AppService = AppService;
            _comService = comService;
            _specAppService = specAppService;
            _copAppService = copAppService;
            _memAppService = memAppService;
            _parService = parService;
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
            ViewBag.PayType = "";
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

        public async Task<ActionResult> Add(long goodsId, long memberId)
        {
            DataTable dt = await _specAppService.GetObjTableBuyObjId(goodsId);
            if (dt.Rows.Count == 0 || dt == null)//商品如果没有规格
            {
                DataRow dr = dt.NewRow();
                var goods = await _AppService.GetByIdAsync(goodsId);
                dr["Id"] = goodsId;
                dr["Text"] = "--";
                dr["Stock"] = goods.Stock;
                dr["Price"] = goods.Price;
                dt.Rows.Add(dr);
            }


            ViewBag.Member = await _memAppService.GetEntity(memberId);
            decimal pp = Convert.ToDecimal(await _parService.GetParameterValue("PointPayPrice"));
            ViewBag.MaxIntegralToMoney = ((decimal)(ViewBag.Member.Integral / pp)).ToString("#0.00");

            ViewBag.PayType = await _parService.GetParameterValue("PayType");
            ViewBag.Coupon = _copAppService.GetUseableCoupon(memberId, (decimal)dt.Rows[0]["Price"]);
            return View("_Select", dt);
        }
    }
}