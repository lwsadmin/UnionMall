using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Goods;
using UnionMall.Controllers;
using X.PagedList;
using UnionMall.Common;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsComment")]
    public class GoodsCommentController : UnionMallControllerBase
    {
        private readonly IGoodsComAppService _Service;
        private readonly ICommonAppService _comService;
        public GoodsCommentController(IGoodsComAppService Service, ICommonAppService comService)
        {
            _comService = comService;
            _Service = Service;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string title = "")
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and a.name like '%{title}%' ";
            int total;
            DataSet ds = _Service.GetPage(page, pageSize, "c.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }

        public async Task<IActionResult> Get(long id)
        {
            var s = await _Service.GetComByParentIdAsync(id);
            return new JsonResult(new { s = s });
        }
    }
}