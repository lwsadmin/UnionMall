﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Gift;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GiftMall.Controllers
{
    [Area("GiftMall")]
    [AbpMvcAuthorize("GiftMall.GiftOrder")]
    public class GiftOrderController : UnionMallControllerBase
    {
        private readonly ICommonCategoryAppService _catAppService;
        private readonly IImageAppService _imgAppService;
        private readonly IGiftOrderAppService _AppService;
        private readonly IGiftOrderItemAppService _itemAppService;
        private readonly ICommonAppService _comService;
        public GiftOrderController(ICommonCategoryAppService catAppService,
            ICommonAppService comService, IGiftOrderAppService AppService,
            IGiftOrderItemAppService itemAppService,
            IImageAppService imgAppService)
        {
            _catAppService = catAppService;
            _comService = comService;
            _AppService = AppService;
            _itemAppService = itemAppService;
            _imgAppService = imgAppService;
        }

        public async Task<IActionResult> List(string timeFrom, string timeTo, string orderNumber, string cardId,
            string name, string status, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(name))
                where += $" and name like '%{name}%' ";
            if (!string.IsNullOrEmpty(status))
                where += $" and o.Status={status} ";
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber = '{orderNumber}' ";
            if (!string.IsNullOrEmpty(cardId))
                where += $" and cardId like '%{cardId}%' ";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and o.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and o.CreationTime<='{timeTo} 23:59:59'";
            int total;

            DataSet ds = _AppService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            ViewBag.Category = await _catAppService.GetCategoryDropDownList(0, 1);
            return View(pageList);
        }
        [HttpPost]
        public JsonResult GetOrderItem(long orderId)
        {
            var catList = _itemAppService.GetOrderDetail(orderId);
            return Json(new { data = catList });
        }
        public async Task<FileResult> ExportExcel(string timeFrom, string timeTo, string orderNumber, string cardId,
            string name, string status)
        {

            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(name))
                where += $" and name like '%{name}%' ";
            if (!string.IsNullOrEmpty(status))
                where += $" and o.Status={status} ";
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber = '{orderNumber}' ";
            if (!string.IsNullOrEmpty(cardId))
                where += $" and cardId like '%{cardId}%' ";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and o.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and o.CreationTime<='{timeTo} 23:59:59'";

            MemoryStream ms = await _AppService.ExportToExcel(where);
            return File(ms.ToArray(), "application/vnd.ms-excel", "" + L("GiftOrder") + ".xlsx");
        }
    }
}