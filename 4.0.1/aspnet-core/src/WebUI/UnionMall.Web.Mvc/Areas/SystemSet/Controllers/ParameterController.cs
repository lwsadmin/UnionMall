using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.SystemSet;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize("SystemSet.OperateSet")]
    public class ParameterController : UnionMallControllerBase
    {
        private readonly IParameterAppService _AppService;
        private readonly ICommonAppService _comService;
        public ParameterController(IParameterAppService AppService, ICommonAppService comService)
        {
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}