using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Member;
using X.PagedList;

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

        public async Task<IActionResult> Index()
        {
         //   ViewBag.Page = new PagedList<DataRow>(null, 1, 1);
            return View();
        }
        [AbpMvcAuthorize("UnionMember.CardInfo")]
        public async Task<JsonResult> GetCardInfo(string cardId)
        {
            var m = await _AppService.GetCardCore(cardId);
            if (m == null)
            {
                return new JsonResult(new { succ = false, member = m });
            }
            else
            {
                return new JsonResult(new { succ = true, member = m });
            }

        }
    }
}