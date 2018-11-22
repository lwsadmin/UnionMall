using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Business;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Coupon;
using UnionMall.Coupon.ReceiveStatistics;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.CouponUseStatistics")]
    [Area("Statistics")]
    public class UsedStatisticsController : UnionMallControllerBase
    {
        private readonly IUsedStatisticsAppService _appService;
        private readonly ICommonAppService _comService;
        private readonly IChainStoreAppService _storeAppService;
        public UsedStatisticsController(IUsedStatisticsAppService appService,
            IChainStoreAppService storeAppService, ICommonAppService comService)
        {
            _appService = appService;
            _storeAppService = storeAppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string title = "",
            string timeFrom = "", string timeTo = "")
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(title))
                where += $" and title like '%{title}%'";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and SendTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and SendTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _appService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));
            return View(pageList);
        }
    }
}