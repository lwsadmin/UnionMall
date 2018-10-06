using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business.Buiness;
using UnionMall.Business.Buiness.Dto;
using UnionMall.Business.ChainStore;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Business.Controllers
{
    [Area("Business")]
    [AbpMvcAuthorize]
    public class ChainStoreController : UnionMallControllerBase
    {
        private readonly IBusinessAppService _businessAppService;
        private readonly IChainStoreAppService _AppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IAbpSession _AbpSession;
        public ChainStoreController(IChainStoreAppService AppService, IAbpSession abpSession,
            IBusinessAppService businessAppService, ICommonAppService commonAppService)
        {
            _commonAppService = commonAppService;
            _AppService = AppService;
            _AbpSession = abpSession;
            _businessAppService = businessAppService;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string storeName = "")
        {
            string table = $@"select c.id,c.BusinessId,c.Name,b.BusinessName, c.Image,c.Mobile,c.CreationTime,c.Contact,c.Sort from TChainStore c
left join TBusiness b on c.BusinessId = b.Id";
            if (_AbpSession.TenantId != null)
            {
                table += $" where c.TenantId={_AbpSession.TenantId}";
            }
            if (!string.IsNullOrEmpty(storeName))
            { table += $" and c.Name like '%{storeName}%'"; }
            int total;

            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);

            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            return View(pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            if (id == null)
            {
                var s = new ChainStore();
                s.Id = 0;
                s.TenantId = 0;
                if (_AbpSession.TenantId != null && (int)AbpSession.TenantId > 0)
                    s.TenantId = (int)_AbpSession.TenantId;
                return PartialView("_Add", s);
            }
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            var dtos = await _AppService.GetByIdAsync((long)id);
            return PartialView("_Add", dtos);
        }
    }
}