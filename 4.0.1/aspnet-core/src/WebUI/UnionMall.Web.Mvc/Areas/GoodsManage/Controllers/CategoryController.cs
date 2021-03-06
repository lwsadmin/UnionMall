﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Goods;
using UnionMall.Goods.Dto;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize]
    public class CategoryController : UnionMallControllerBase
    {
        private readonly IGoodsCategoryAppService _AppService;
        private readonly IBrandAppService _brandAppService;
        private readonly IAbpSession _AbpSession;
        public CategoryController(IGoodsCategoryAppService AppService,
            IBrandAppService brandAppService,
            IAbpSession abpSession)
        {
            _AppService = AppService;
            _brandAppService = brandAppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page = 1)
        {
            int pageSize = 10;
            string table = $"select g.Id,g.Title,g.ParentId,g.Sort,g.Note from TGoodsCategory g where g.ParentId=0";
            if (_AbpSession.TenantId != null)
                table += $" and g.TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "sort desc, id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            List<DropDownDto> dtoList = _AppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("cat", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "Title"));

            List<BrandSelectDto> t = _brandAppService.GetMultiSelect();
            ViewData.Add("brand", t);

            return View(pageList);
        }

        public IActionResult Table(int page = 1, int pageSize = 10)
        {
            string table = $"select g.Id,g.Title,g.ParentId,g.Sort,g.Note from TGoodsCategory g where g.ParentId=0";
            if (_AbpSession.TenantId != null)
                table += $" and g.TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "sort desc, id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            return View("_Table", pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            List<DropDownDto> dtoList = _AppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("cat", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "Title"));

            List<BrandSelectDto> d = _brandAppService.GetMultiSelect();
            ViewData.Add("brand", d);
            if (id == null)
            {
                var s = new CategoryEditDto();
                s.Id = 0;
                s.TenantId = _AbpSession.TenantId;
                return PartialView("_Add", s);
            }

            var dtos = await _AppService.GetByIdAsync((long)id);
            return PartialView("_Add", dtos);
        }

        [HttpPost]
        public async Task<JsonResult> GetChildCategory(long parentId)
        {
            var catList = await _AppService.GetAllListByParentIdAsync(parentId);
            return Json(new { data = catList });
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            string msg = string.Empty;
            var booler = _AppService.Delete(id, out msg);
            return Json(new { succ = booler, msg = L(msg) });
        }
    }
}