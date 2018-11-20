using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Coupon;
using UnionMall.Coupon.ReceiveStatistics;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{


    public class SendStatisticsController : UnionMallControllerBase
    {
        private readonly IReceiveStatisticsAppService _appService;
        private readonly ICommonAppService _comService;

        public SendStatisticsController(IReceiveStatisticsAppService appService, ICommonAppService comService)
        {
            _appService = appService;
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
                return View("_Table", "");
            }
            return View();
        }
    }
}