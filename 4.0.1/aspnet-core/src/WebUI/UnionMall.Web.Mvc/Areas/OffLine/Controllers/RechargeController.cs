using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Authorization.Accounts;
using UnionMall.Business;
using UnionMall.Controllers;
using UnionMall.Member;
using UnionMall.Sessions;

namespace UnionMall.Web.Mvc.Areas.OffLine.Controllers
{
    [Area("OffLine")]
    [AbpMvcAuthorize("OffLineCashie.MemberRecharge")]
    public class RechargeController : UnionMallControllerBase
    {
        private readonly IChainStoreAppService _storeAppService;
        private readonly IAbpSession _AbpSession;
        private readonly ISessionAppService _sessionAppService;
        private readonly IAccountAppService _userAccountManager;
        private readonly IMemberAppService _memberService;
        public RechargeController(IChainStoreAppService storeAppService, IAbpSession AbpSession,
            ISessionAppService sessionAppService, IMemberAppService memberService,
            IAccountAppService userAccountManager)
        {
            _storeAppService = storeAppService;
            _AbpSession = AbpSession;
            _userAccountManager = userAccountManager;
            _sessionAppService = sessionAppService;

        }
        public async Task<IActionResult> Index()
        {
            //var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
            //var s = await _storeAppService.GetByIdAsync(UserInfo.User.ChainStoreId);
            return View();
        }

        //[HttpPost]
        //[Abp.Domain.Uow.UnitOfWork]
        //public async Task<ActionResult> Submit(long memberId)
        //{
        //    var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
        //    var s = await _memberService.MemberRecharge(memberId,1, UserInfo.User);
        //    return s;
        //}
    }
}