using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Article;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;
using UnionMall.Entity;
namespace UnionMall.Web.Mvc.Areas.ArticleInfo.Controllers
{
    [Area("ArticleInfo")]
    [AbpMvcAuthorize("ArticleInfo.Article")]
    public class ArticleController : UnionMallControllerBase
    {
        private readonly ICommonCategoryAppService _catAppService;
        private readonly IArticleAppService _AppService;
        private readonly ICommonAppService _comService;
        public ArticleController(ICommonCategoryAppService catAppService, IArticleAppService AppService, ICommonAppService comService)
        {
            _catAppService = catAppService;
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string categoryId = "", string title = "")
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and a.title like '%{title}%' ";
            if (!string.IsNullOrEmpty(categoryId))
                where += $" and a.categoryId ={categoryId} ";
            int total;
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "c.sort desc, c.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }

            ViewBag.Category = await _catAppService.GetCategoryDropDownList();
            return View(pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            Entity.Article a = new Entity.Article();
            if (id != null)
            {
                a = await _AppService.GetByIdAsync((long)id);
            }

            var list = await _catAppService.GetCategoryDropDownList();
            ViewData.Add("Category", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(list, "Id", "Title"));
            return View(a);
        }
    }
}