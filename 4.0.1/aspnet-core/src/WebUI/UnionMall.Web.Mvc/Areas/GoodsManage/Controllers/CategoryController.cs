using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Goods.GoodsCategory;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize]
    public class CategoryController : UnionMallControllerBase
    {
        private readonly IGoodsCategoryAppService _AppService;
        private readonly IAbpSession _AbpSession;
        public CategoryController(IGoodsCategoryAppService AppService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page = 1)
        {
            int pageSize = 10;
            string table = $"select *from tgoodscategory";
            if (_AbpSession.TenantId != null)
                table += $" where TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            return View(pageList);
        }

        public IActionResult Table(int page = 1,int pageSize= 10)
        {
            string table = $"select *from tgoodscategory";
            if (_AbpSession.TenantId != null)
                table += $" where TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            return View("_Table", pageList);
        }
    }
}