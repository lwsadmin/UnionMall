using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.BalanceNote;
using UnionMall.Business;
using UnionMall.Common;
using UnionMall.ConsumeNote;
using UnionMall.Controllers;
using UnionMall.Coupon;
using UnionMall.Goods;
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
        private readonly ICouponAppService _couponService;
        private readonly IConsumeNoteAppService _consumeService;
        private readonly IIntegralNoteAppService _integralService;
        private readonly IBalanceNoteAppService _balanceService;
        private readonly IUsedStatisticsAppService _useService;
        private readonly IGoodsOrderAppService _orderAppService;
        private readonly IMemberLevelAppService _levelAppService;
        private readonly IChainStoreAppService _storeAppService;
        public CardCoreController(IMemberAppService AppService,
            IConsumeNoteAppService consumeService,
            ICommonAppService comAppService, IIntegralNoteAppService integralService, IGoodsOrderAppService orderAppService,
            IBalanceNoteAppService balanceService, IUsedStatisticsAppService useService,
            IMemberLevelAppService levelAppService, CouponAppService couponService,
            IChainStoreAppService storeAppService)
        {
            _AppService = AppService;
            _consumeService = consumeService;
            _comAppService = comAppService;
            _integralService = integralService;
            _balanceService = balanceService;
            _orderAppService = orderAppService;
            _useService = useService;
            _levelAppService = levelAppService;
            _storeAppService = storeAppService;
            _couponService = couponService;
        }

        public async Task<IActionResult> Index(long? id)
        {
            //   ViewBag.Page = new PagedList<DataRow>(null, 1, 1);
            if (id != null)
            {
                ViewBag.CarId = (await _AppService.GetEntity((long)id)).CardID;
            }

            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewData.Add("Level", new SelectList(levelDropDown, "Id", "Title"));

            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));

            string where = _comAppService.GetWhere();
            int total;
            string sql = $@"select c.id,c.Title from dbo.TCoupon c where c.id>0";
            DataSet ds = _couponService.GetPage(1, int.MaxValue, "id desc", out total, where, sql);
            ViewBag.CouponTable = ds.Tables[0];
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
        public async Task<IActionResult> OrderList(int page = 1, int pageSize = 10, string memberId = "")
        {
            string where = $" and Memberid={memberId}";
            int total;
            DataSet ds = _orderAppService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_TableOrder", pageList);
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