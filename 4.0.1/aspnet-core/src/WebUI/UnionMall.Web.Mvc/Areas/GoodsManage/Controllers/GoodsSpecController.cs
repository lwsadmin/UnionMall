using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common.CommonSpec;
using UnionMall.Controllers;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsSpec")]
    public class GoodsSpecController : UnionMallControllerBase
    {
        private readonly ICommonSpecAppService _AppService;

        public GoodsSpecController(ICommonSpecAppService AppService)
        {
            _AppService = AppService;
        }
        public async Task<IActionResult> List(string name, int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(name))
            { where = $" and s.Name like '%{name}%'"; }
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, " ", out total, where);
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