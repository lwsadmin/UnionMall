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
    public class PaySetController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        public PaySetController(IParameterAppService appService)
        {
            _appService = appService;
        }
        public async Task<IActionResult> Index()
        {

            ViewData["mch_id"] = await _appService.GetParameter("mch_id");
            ViewData["server_mch_id"] = await _appService.GetParameter("server_mch_id");
            ViewData["IsBusinessCollection"] = await _appService.GetParameter("IsBusinessCollection");
            ViewData["payKey"] = await _appService.GetParameter("payKey");
            ViewData["partner"] = await _appService.GetParameter("partner");
            ViewData["private_key"] = await _appService.GetParameter("private_key");
            ViewData["merchant_private_appid"] = await _appService.GetParameter("merchant_private_appid");
            ViewData["merchant_private_key"] = await _appService.GetParameter("merchant_private_key");
            ViewData["alipay_server_appid"] = await _appService.GetParameter("alipay_server_appid");
            ViewData["alipay_service_provider_id"] = await _appService.GetParameter("alipay_service_provider_id");
            ViewData["alipay_server_key"] = await _appService.GetParameter("alipay_server_key");
            ViewData["alipay_server_public_key"] = await _appService.GetParameter("alipay_server_public_key");
            ViewData["WeiXinLEC"] = await _appService.GetParameter("WeiXinLEC");
            return View();
        }
    }
}