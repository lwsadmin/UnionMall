using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Settlement.Controllers
{
    [Area("Settlement")]
    [AbpMvcAuthorize("FundSettle.StoreDivideSet")]
    public class ChainStoreSetController : UnionMallControllerBase
    {
        private readonly IBusinessAppService _businessAppService;
        private readonly IChainStoreAppService _AppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IAbpSession _AbpSession;
        public ChainStoreSetController(IChainStoreAppService AppService, IAbpSession abpSession,
            IBusinessAppService businessAppService, ICommonAppService commonAppService)
        {
            _commonAppService = commonAppService;
            _AppService = AppService;
            _AbpSession = abpSession;
            _businessAppService = businessAppService;
        }

        public async Task<IActionResult> List(int page = 1, int pageSize = 10,
             string storeName = "", string businessId = "")
        {
            string where = _commonAppService.GetWhere();
            if (!string.IsNullOrEmpty(storeName))
            { where = $" and c.Name like '%{storeName}%'"; }
            if (!string.IsNullOrEmpty(businessId))
            { where += $" and c.BusinessId = {businessId}"; }
            int total;
            string table = $@"select c.id, c.Name,c.IsSystem, cast(c.OnLineCommission as float) OnLineCommission
,cast(c.OffLineCommission as float) OffLineCommission,cast(c.OffLineDiscount as float) OffLineDiscount from TChainStore c where 1=1";
            DataSet ds = _AppService.GetPage(page, pageSize, "c.sort desc, c.id desc", out total, where, table);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            return View(pageList);
        }
        [HttpPost]
        public JsonResult Save(long pk, string value, string name)
        {
            _AppService.SaveCmn(name, value, pk);
            return Json(new { });
        }
    }
}