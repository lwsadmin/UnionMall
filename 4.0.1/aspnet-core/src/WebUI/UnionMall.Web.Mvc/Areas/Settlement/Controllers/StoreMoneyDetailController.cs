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
using UnionMall.Settlement;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Settlement.Controllers
{
    [Area("Settlement")]
    [AbpMvcAuthorize("FundSettle.FinancialDetail")]
    public class StoreMoneyDetailController : UnionMallControllerBase
    {
        private readonly IBusinessAppService _businessAppService;
        private readonly IChainStoreAppService _AppService;
        private readonly ICommonAppService _commonAppService;
        private readonly ICapitalNoteAppService _capService;
        public StoreMoneyDetailController(IChainStoreAppService AppService, ICapitalNoteAppService capService,
            IBusinessAppService businessAppService, ICommonAppService commonAppService)
        {
            _commonAppService = commonAppService;
            _AppService = AppService;
            _capService = capService;
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
            DataSet ds = _capService.GetPage(page, pageSize, "c.sort desc, c.id desc", out total, where);
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
    }
}