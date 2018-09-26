using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Goods.GoodsCategory;
using UnionMall.Goods.GoodsCategory.Dto;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize]
    public class CategoryController : UnionMallControllerBase
    {
        private readonly IGoodsCategoryAppService _AppService;
        private readonly IAbpSession _AbpSession;
        public CategoryController(IGoodsCategoryAppService AppService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int page = 1)
        {
            int pageSize = 10;
            string table = $"select g.Id,g.Title,g.ParentId,g.Sort,g.Note from TGoodsCategory g where g.ParentId=0";
            if (_AbpSession.TenantId != null)
                table += $" and g.TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            List<DropDownDto> dtoList = _AppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("cat", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "Title"));
            return View(pageList);
        }

        public IActionResult Table(int page = 1, int pageSize = 10)
        {
            string table = $"select g.Id,g.Title,g.ParentId,g.Sort,g.Note from TGoodsCategory g where g.ParentId=0";
            if (_AbpSession.TenantId != null)
                table += $" and g.TenantId={_AbpSession.TenantId}";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, table, "sort asc, id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);

            return View("_Table", pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            List<DropDownDto> dtoList = _AppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            ViewData.Add("cat", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "Title"));
            if (id == null)
            {
                var s = new CategoryEditDto();
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
    }
}