using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Member;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize("SystemSet.OperateSet")]
    public class ParameterController : UnionMallControllerBase
    {
        private readonly IParameterAppService _appService;
        private readonly ICommonAppService _comService;
        private readonly IMemberLevelAppService _levelAppService;
        public ParameterController(IParameterAppService AppService,
            ICommonAppService comService, IMemberLevelAppService levelAppService)
        {
            _appService = AppService;
            _comService = comService;
            _levelAppService = levelAppService;
        }
        public async Task<IActionResult> Index()
        {

            ViewData["MemberDefaultGroup"] = await _appService.GetParameterValue("MemberDefaultGroup");
            ViewData["MemberDefaultPassword"] = await _appService.GetParameterValue("MemberDefaultPassword");
            ViewData["RegAgreement"] = await _appService.GetParameterValue("RegAgreement");
            ViewData["AutoReg"] = await _appService.GetParameterValue("AutoReg"); ;
            ViewData["RegSmsCode"] = await _appService.GetParameterValue("RegSmsCode");

            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewBag.Level = levelDropDown;
            return View();
        }
    }
}