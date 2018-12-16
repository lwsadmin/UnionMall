using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.WeChatSet.Controllers
{
    [Area("WeChatSet")]
    [AbpMvcAuthorize("WeChatSet.WeChatDocking")]
    public class DockingController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        public DockingController(IParameterAppService appService)
        {
            _appService = appService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["appID"] = await _appService.GetParameterValue("AppID");
            ViewData["appsecret"] = await _appService.GetParameterValue("Appsecret");


            ViewData["IsWeiXinDevelopment"] = await _appService.GetParameterValue("IsWeiXinDevelopment");
            ViewData["url"] = await _appService.GetParameterValue("Url"); ;
            ViewData["token"] = await _appService.GetParameterValue("Token");
            //ViewData["weiXinType"] = await _appService.GetParameterValue("WeiXinType");
            ViewData["AttentionReply"] = await _appService.GetParameterValue("AttentionReply");
            return View();
        }
    }
}