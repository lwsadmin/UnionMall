using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;

namespace UnionMall.Web.Mvc.Areas.ArticleInfo.Controllers
{
    public class CommentController : UnionMallControllerBase
    {
        private readonly ICommonAppService _comService;
        public CommentController(ICommonAppService comService)
        {
            _comService = comService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string categoryId = "", string title = "")
        {
            return View();
        }
    }
}