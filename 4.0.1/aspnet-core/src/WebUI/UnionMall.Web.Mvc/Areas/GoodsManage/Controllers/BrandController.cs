using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Goods.Brand;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize]
    public class BrandController : UnionMallControllerBase
    {
        private readonly IBrandAppService _AppService;
        private readonly IAbpSession _AbpSession;
        public BrandController(IBrandAppService AppService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page=1, int pageSize = 10)
        {
            
            string table = $"select b.id,b.Title,b.Url, b.TenantId,b.Logo,b.Sort,b.Note from dbo.TBrand b";
            if (_AbpSession.TenantId != null&& (int)AbpSession.TenantId>0)
                table += $" where b.TenantId={_AbpSession.TenantId}";
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
        public IActionResult Table(int page = 1, int pageSize = 10)
        {
            string table = $"select b.id,b.Title,b.Url,b.TenantId,b.Logo,b.Sort,b.Note from dbo.TBrand b";
            if (_AbpSession.TenantId != null && (int)AbpSession.TenantId > 0)
                table += $" where  b.TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            return View("_Table", pageList);
        }
    }
}