using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Member;
using UnionMall.Settlement;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.Settlement.Controllers
{
    [Area("Settlement")]
    [AbpMvcAuthorize("FundSettle.AllSettleSet")]
    public class GlobalController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        private readonly IMemberLevelAppService _levelAppService;
        private readonly IGlobalAppService _globalAppService;
        public GlobalController(IParameterAppService appService, IMemberLevelAppService levelAppService,
            IGlobalAppService globalAppService)
        {
            _appService = appService;
            _levelAppService = levelAppService;
            _globalAppService = globalAppService;
        }

        public async Task<IActionResult> Index()
        {
            // ViewData["SettleType"] = await _appService.GetParameterValue("SettleType");
            ViewData["PointPayPrice"] = await _appService.GetParameterValue("PointPayPrice");
            ViewData["CouponPaidForSettlement"] = await _appService.GetParameterValue("CouponPaidForSettlement");
            ViewData["PointPaidForSettlement"] = await _appService.GetParameterValue("PointPaidForSettlement");
            int total;
            string table = $"select m.id,m.Title, cast(m.profit as float) profit from TMemberLevel m where 1=1 ";
            ViewData["cat"] = _levelAppService.GetPage(1, int.MaxValue, "", out total, "", table).Tables[0];
            ViewBag.Global = await _globalAppService.GetGlobalSet();
            return View();
        }
        [HttpPost]
        public JsonResult Save(long pk, decimal value)
        {
            _levelAppService.SaveProfit(value, pk);
            return Json(new { });
        }

        [HttpPost]
        public JsonResult SaveGlobal(string pk, decimal value)
        {
            _globalAppService.SaveGlobal(pk,value);
            return Json(new { });
        }
    }
}