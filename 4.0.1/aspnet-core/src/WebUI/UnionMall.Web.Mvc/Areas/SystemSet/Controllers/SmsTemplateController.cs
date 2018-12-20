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
    [AbpMvcAuthorize("SystemSet.SmsTemplate")]
    public class SmsTemplateController : UnionMallControllerBase
    {
        private readonly ISmsTemplateAppService _AppService;
        private readonly ICommonAppService _comService;
        public SmsTemplateController(ISmsTemplateAppService AppService, ICommonAppService comService)
        {
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(string title, int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(title))
                where += $" and t.title like '%{title}%' ";
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "id desc", out total, where);
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