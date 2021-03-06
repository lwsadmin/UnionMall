﻿using System;
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
using UnionMall.Core;
using UnionMall.Entity;
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
        [AbpMvcAuthorize]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10,
             string storeName = "", string businessId = "")
        {
            string where = _commonAppService.GetWhere();
            if (!string.IsNullOrEmpty(storeName))
            { where = $" and c.Name like '%{storeName}%'"; }
            if (!string.IsNullOrEmpty(businessId))
            { where += $" and c.BusinessId = {businessId}"; }
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "c.sort desc, c.id desc", out total, where);
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

        [HttpPost]
        public JsonResult GetAddressSlect(int id, int type)
        {
            string option = string.Empty;
            switch (type)
            {
                case 0:
                    option = AddressSelect.GetCity(id.ToString());
                    break;
                case 1:
                    option = AddressSelect.GetDis(id.ToString());
                    break;

            }
            return Json(new { succ = true, str = option });
        }
        [HttpPost]
        public JsonResult Delete(long id)
        {
            string msg = string.Empty;
            var booler = _AppService.Delete(id, out msg);
            if (msg == "ExistRecord")
            {
                return Json(new { succ = booler, msg = L("ExistRecord{0}", L("Store")) });
            }
            return Json(new { succ = booler, msg = L(msg) });
        }
    }
}