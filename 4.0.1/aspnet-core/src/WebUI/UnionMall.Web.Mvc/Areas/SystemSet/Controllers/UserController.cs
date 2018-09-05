using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using Castle.Core.Logging;
using UnionMall.Users;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize]
    public class UserController : UnionMallControllerBase
    {
        private readonly IUserAppService _userAppService;
        public readonly ILogger _logger;
        public UserController(IUserAppService userAppService, ILogger logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        public IActionResult List()
        {
            Logger.Debug("-------------------");
            return View();
        }
    }
}