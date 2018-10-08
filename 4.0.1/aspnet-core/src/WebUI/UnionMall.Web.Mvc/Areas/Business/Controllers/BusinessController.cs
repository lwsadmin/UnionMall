using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using model = UnionMall.Business.Business;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;
using UnionMall.Business.Business;

namespace UnionMall.Web.Mvc.Areas.Business.Controllers
{
    [Area("Business")]
    [AbpMvcAuthorize]
    public class BusinessController : UnionMallControllerBase
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IAbpSession _AbpSession;
        public BusinessController(ICommonAppService commonAppService, IAbpSession abpSession,
            IBusinessAppService businessAppService)
        {
            _commonAppService = commonAppService;
            _AbpSession = abpSession;
            _businessAppService = businessAppService;
        }
        public IActionResult List(int page = 1, int pageSize = 10, string BusinessName = "")
        {
            string table = $@"select b.Id,b.Sort,b.StoreCount, b.Tel,b.BusinessName,b.Memo,b.issystemBusiness,
CONVERT(varchar(100), b.DueTime, 23) DueTime,b.Contact from TBusiness b";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            if (!string.IsNullOrEmpty(BusinessName))
            { table += $" and b.BusinessName like '%{BusinessName}%'"; }
            int total;
            DataSet ds = _commonAppService.GetPage(page, pageSize, table, "sort desc, id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            if (id == null)
            {
                var s = new model.Business();
                s.Id = 0;
                s.TenantId = 0;
                if (_AbpSession.TenantId != null && (int)AbpSession.TenantId > 0)
                    s.TenantId = (int)_AbpSession.TenantId;
                return PartialView("_Add", s);
            }
            var dtos = await _businessAppService.GetByIdAsync((long)id);
            return PartialView("_Add", dtos);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            string msg = string.Empty;
            var booler = _businessAppService.Delete(id, out msg);
            if (msg == "ExistRecord")
            {
                return Json(new { succ = booler, msg = L("ExistRecord{0}", L("Store")) });
            }
            return Json(new { succ = booler, msg = L(msg) });
        }
    }
}