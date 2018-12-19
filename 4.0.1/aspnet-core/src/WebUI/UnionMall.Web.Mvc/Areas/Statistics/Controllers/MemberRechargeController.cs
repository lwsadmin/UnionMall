using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Statistics;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.MemberConsumeStatistics")]
    [Area("Statistics")]
    public class MemberRechargeController : UnionMallControllerBase
    {
        private readonly IRechargeNoteAppService _appService;
        private readonly ICommonAppService _comService;
        public MemberRechargeController(IRechargeNoteAppService appService, ICommonAppService comService)
        {
            _appService = appService;
            _comService = comService;
        }
        public async Task<IActionResult> List(string orderNumber, string name,
            string timeFrom, string timeTo, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and n.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and n.CreationTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _appService.GetPage(page, pageSize, "n.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }

        public async Task<IActionResult> TableChat(string orderNumber, string name, string timeFrom, string timeTo)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and n.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and n.CreationTime<='{timeTo} 23:59:59'";

            DataTable dt = _appService.GetTotalData(where).Tables[0];

            string str = $"'{L("Total")}','{L("WeChat")}','{L("AliPay")}','{L("Cash")}'";

            ViewBag.Cat = str;
            ViewBag.Data= $"{dt.Rows[0]["TotalPay"]},{dt.Rows[0]["WeChatPay"]},{dt.Rows[0]["AliPay"]},{dt.Rows[0]["CashPay"]}";
            return View("_TableChat");

        }
    }
}