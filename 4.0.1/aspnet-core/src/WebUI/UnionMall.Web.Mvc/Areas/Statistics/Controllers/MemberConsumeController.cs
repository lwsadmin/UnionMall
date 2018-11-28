using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.ConsumeNote;
using UnionMall.Controllers;
using X.PagedList;
namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.MemberConsumeStatistics")]
    [Area("Statistics")]
    public class MemberConsumeController : UnionMallControllerBase
    {
        private readonly IConsumeNoteAppService _appService;
        private readonly ICommonAppService _comService;
        public MemberConsumeController(IConsumeNoteAppService appService, ICommonAppService comService)
        {
            _appService = appService;
            _comService = comService;
        }

        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string orderNumber = "",
           string timeFrom = "", string timeTo = "")
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and orderNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and CreationTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _appService.GetPage(page, pageSize, "CreationTime desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}