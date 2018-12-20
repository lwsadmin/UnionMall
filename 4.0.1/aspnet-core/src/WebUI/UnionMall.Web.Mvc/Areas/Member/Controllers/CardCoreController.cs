using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.BalanceNote;
using UnionMall.Common;
using UnionMall.ConsumeNote;
using UnionMall.Controllers;
using UnionMall.Coupon;
using UnionMall.IntegralNote;
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
        private readonly IConsumeNoteAppService _consumeService;
        private readonly IIntegralNoteAppService _integralService;
        private readonly IBalanceNoteAppService _balanceService;
        private readonly IUsedStatisticsAppService _useService;
        public CardCoreController(IMemberAppService AppService, 
            IConsumeNoteAppService consumeService, 
            ICommonAppService comAppService, IIntegralNoteAppService integralService,
            IBalanceNoteAppService balanceService, IUsedStatisticsAppService useService)
        {
            _AppService = AppService;
            _consumeService = consumeService;
            _comAppService = comAppService;
            _integralService = integralService;
            _balanceService = balanceService;
            _useService = useService;
        }

        public async Task<IActionResult> Index(long? id)
        {
            //   ViewBag.Page = new PagedList<DataRow>(null, 1, 1);
            if (id != null)
            {
                ViewBag.CarId = (await _AppService.GetEntity((long)id)).CardID;
            }

            return View();
        }

        public async Task<IActionResult> ConsumeList(int page = 1, int pageSize = 10, string memberId = "")
        {
            string where = $" and Memberid={memberId}";
            int total;
            DataSet ds = _consumeService.GetPage(page, pageSize, "n.CreationTime desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_TableConsume", pageList);
        }
        public async Task<IActionResult> BalanceList(int page = 1, int pageSize = 10, string memberId = "")
        {
            string where = $" and Memberid={memberId}";
            int total;
            DataSet ds = _balanceService.GetPage(page, pageSize, "n.CreationTime desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_TableBalance", pageList);
        }

        public async Task<IActionResult> IntegralList(int page = 1, int pageSize = 10, string memberId = "")
        {
            string where = $" and Memberid={memberId}";
            int total;
            DataSet ds = _integralService.GetPage(page, pageSize, "n.CreationTime desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_TableIntegral", pageList);
        }
        public async Task<IActionResult> CouponList(int page = 1, int pageSize = 10, string memberId = "")
        {
            string where = $" and Memberid={memberId}";
            int total;
            DataSet ds = _useService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_TableCoupon", pageList);
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