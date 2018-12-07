using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.SystemSet;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize("SystemSet.LogList")]
    public class LogController : UnionMallControllerBase
    {
        private readonly ILogAppService _AppService;
        private readonly ICommonAppService _comService;
        public LogController(ILogAppService AppService, ICommonAppService comService)
        {
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(string timeFrom, string timeTo,string user, string content, int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(content))
                where += $" and content like '%{content}%' ";
            if (!string.IsNullOrEmpty(user))
                where += $" and UserAcccount like '%{user}%' ";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and CreationTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "l.id desc", out total, where);
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