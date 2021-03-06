﻿using System;
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
    [AbpMvcAuthorize("StatisticsAnalysis.MemberIntegralStatistics")]
    [Area("Statistics")]
    public class LevelUpdateController : UnionMallControllerBase
    {
        private readonly ILevelUpdateAppService _appService;
        private readonly ICommonAppService _comService;
        public LevelUpdateController(ILevelUpdateAppService appService, ICommonAppService comService)
        {
            _appService = appService;
            _comService = comService;
        }

        public async Task<IActionResult> List(string orderNumber, string way,
       string name, string cardid, string timeFrom, string timeTo, int page = 1, int pageSize = 10)
        {

            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(cardid))
                where += $" and cardid like '%{cardid}%'";
            if (!string.IsNullOrEmpty(way))
                where += $" and way ={way}";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and u.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and u.CreationTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _appService.GetPage(page, pageSize, "u.id desc", out total, where);
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