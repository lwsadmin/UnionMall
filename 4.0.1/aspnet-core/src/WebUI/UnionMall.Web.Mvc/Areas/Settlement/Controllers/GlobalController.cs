using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.Settlement.Controllers
{
    [Area("Settlement")]
    [AbpMvcAuthorize("FundSettle.AllSettleSet")]
    public class GlobalController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        public GlobalController(IParameterAppService appService)
        {
            _appService = appService;
        }

        public async Task<IActionResult> Index()
        {
           // ViewData["SettleType"] = await _appService.GetParameterValue("SettleType");
            ViewData["PointPayPrice"] = await _appService.GetParameterValue("PointPayPrice");
            ViewData["CouponPaidForSettlement"] = await _appService.GetParameterValue("CouponPaidForSettlement");
            ViewData["PointPaidForSettlement"] = await _appService.GetParameterValue("PointPaidForSettlement"); ;
            return View();
        }
    }
}