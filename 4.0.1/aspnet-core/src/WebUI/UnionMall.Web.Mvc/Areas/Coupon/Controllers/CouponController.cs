using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Controllers;
using UnionMall.Coupon;
using X.PagedList;
using UnionMall.Coupon.Dto;
using UnionMall.Common;

namespace UnionMall.Web.Mvc.Areas.Coupon.Controllers
{
    [Area("Coupon")]
    [AbpMvcAuthorize("Coupon")]
    public class CouponController : UnionMallControllerBase
    {
        private readonly ICounponAppService _couponService;
        private readonly IBusinessAppService _AppService;
        private readonly ICommonAppService _comService;
        public CouponController(CounponAppService couponService, IBusinessAppService AppService, ICommonAppService comService)
        {
            _couponService = couponService;
            _AppService = AppService;
            _comService = comService;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string businessId = "", string title = "")
        {

            string where = " and Type=0 " + _comService.GetWhere();
            if (!string.IsNullOrEmpty(businessId))
                where += $" and c.BusinessId = {businessId}";
            if (!string.IsNullOrEmpty(title))
                where += $" and c.title like '%{title}%'";
            int total;
            DataSet ds = _couponService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<BusinessDropDownDto> businessdtoList = (await _AppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(businessdtoList, "Id", "BusinessName"));
            return View(pageList);
        }
        public async Task<IActionResult> Add(long? id)
        {
            if (id == null)
            {
                var s = new CreateEditDto();
                s.Id = 0;
                s.TenantId = 0;
                return PartialView("_Add", s);
            }
            List<BusinessDropDownDto> dtoList = (await _AppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            var dtos = await _couponService.GetByIdAsync((long)id);
            return PartialView("_Add", dtos);
        }
    }
}