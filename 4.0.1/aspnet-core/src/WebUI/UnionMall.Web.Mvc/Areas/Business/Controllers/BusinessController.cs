using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business.Buiness;
using UnionMall.Controllers;
using X.PagedList;
namespace UnionMall.Web.Mvc.Areas.Business.Controllers
{
    [Area("Business")]
    [AbpMvcAuthorize]
    public class BusinessController : UnionMallControllerBase
    {
        private readonly IBusinessAppService _AppService;
        private readonly IAbpSession _AbpSession;
        public BusinessController(IBusinessAppService AppService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int pageIndex = 1)
        {
            int pageSize = 1;
            string table = $"select *from dbo.TBusiness b  ";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            int total;

            DataSet ds = _AppService.GetPage(pageIndex, pageSize, table, "id desc", out total);

            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
         //   page.TotalItemCount = total;
            return View(page);
        }

        public IActionResult Table(int pageIndex = 1)
        {
            int pageSize = 2;
            string table = $"select *from dbo.TBusiness b  ";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            int total;

            DataSet ds = _AppService.GetPage(pageIndex, pageSize, table, "id desc", out total);

            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            //   page.TotalItemCount = total;
            return PartialView("_Table", page);
        }
    }
}