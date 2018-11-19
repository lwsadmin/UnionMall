using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Member;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("UnionMember.CardInfo")]
    public class CardCoreController : UnionMallControllerBase
    {
        private readonly IMemberAppService _AppService;
        private readonly ICommonAppService _comAppService;

        public CardCoreController(IMemberAppService AppService, ICommonAppService comAppService)
        {
            _AppService = AppService;
            _comAppService = comAppService;
        }

        public IActionResult Index()
        {


            return View();
        }
    }
}