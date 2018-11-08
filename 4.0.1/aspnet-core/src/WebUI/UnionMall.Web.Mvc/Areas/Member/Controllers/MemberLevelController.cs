using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Member;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("Member")]
    public class MemberLevelController : UnionMallControllerBase
    {
        private readonly IMemberLevelAppService _AppService;
        public MemberLevelController(IMemberLevelAppService AppService)
        {
            _AppService = AppService;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}