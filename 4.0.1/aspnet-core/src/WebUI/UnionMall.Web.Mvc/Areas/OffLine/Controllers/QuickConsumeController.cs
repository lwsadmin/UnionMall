﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Authorization.Accounts;
using UnionMall.Authorization.Users;
using UnionMall.Business;
using UnionMall.Controllers;
using UnionMall.Sessions;

namespace UnionMall.Web.Mvc.Areas.OffLine.Controllers
{
    [Area("OffLine")]
    [AbpMvcAuthorize("OffLineCashie.QuickConsume")]
    public class QuickConsumeController : UnionMallControllerBase
    {
        private readonly IChainStoreAppService _storeAppService;
        private readonly IAbpSession _AbpSession;
        private readonly ISessionAppService _sessionAppService;
        private readonly IAccountAppService _userAccountManager;
        public QuickConsumeController(IChainStoreAppService storeAppService, IAbpSession AbpSession,
            ISessionAppService sessionAppService,
            IAccountAppService userAccountManager)
        {
            _storeAppService = storeAppService;
            _AbpSession = AbpSession;
            _userAccountManager = userAccountManager;
            _sessionAppService = sessionAppService;

        }
        public async Task<IActionResult> Index()
        {
            var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
            ViewData["discount"] = 10;
            var s = await _storeAppService.GetByIdAsync(UserInfo.User.ChainStoreId);
            if (s != null)
            {
                ViewData["discount"] = s.OffLineDiscount;
            }
            return View();
        }
    }
}