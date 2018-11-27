using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;
using UnionMall.Comment;
namespace UnionMall.Web.Mvc.Areas.ArticleInfo.Controllers
{
    [Area("ArticleInfo")]
    [AbpMvcAuthorize("ArticleInfo.ArticleComment")]
    public class CommentController : UnionMallControllerBase
    {
        private readonly ICommentAppService _Service;
        private readonly ICommonAppService _comService;
        public CommentController(ICommonAppService comService, ICommentAppService Service)
        {
            _comService = comService;
            _Service = Service;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string title = "")
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and a.title like '%{title}%' ";
            int total;
            DataSet ds = _Service.GetPage(pageIndex, pageSize, "c.Praise desc,c.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
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