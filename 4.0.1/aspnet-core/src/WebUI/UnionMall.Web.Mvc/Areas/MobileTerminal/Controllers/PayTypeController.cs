using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.MobileTerminal.Controllers
{
    [Area("MobileTerminal")]
    public class PayTypeController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        public PayTypeController(IParameterAppService appService)
        {
            _appService = appService;
        }
        public async Task<IActionResult> Index()
        {
            //AliPay,WeChat,Balance,Integral,Cash,Coupon
            var s = (await _appService.GetParameter("PayType"));
            ViewData["PayType"] = s.Value;
            ViewBag.P = s;
            return View();
        }
    }
}