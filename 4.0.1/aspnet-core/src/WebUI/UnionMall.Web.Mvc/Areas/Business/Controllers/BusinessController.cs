using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business.Buiness;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;
namespace UnionMall.Web.Mvc.Areas.Business.Controllers
{
    [Area("Business")]
    [AbpMvcAuthorize]
    public class BusinessController : UnionMallControllerBase
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IAbpSession _AbpSession;
        public BusinessController(ICommonAppService commonAppService, IAbpSession abpSession)
        {
            _commonAppService = commonAppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page = 1, int pageSize = 10, string BusinessName = "")
        {
            string table = $"select b.Id,b.Sort,b.StoreCount, b.Tel,b.BusinessName,b.Memo,b.DueTime,b.Contact from TBusiness b";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            if (!string.IsNullOrEmpty(BusinessName))
            { table += $" and b.BusinessName like '%{BusinessName}%'"; }
            int total;
            DataSet ds = _commonAppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}