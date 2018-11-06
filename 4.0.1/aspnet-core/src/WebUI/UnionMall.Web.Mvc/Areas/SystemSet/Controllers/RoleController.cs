using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business.Business;
using UnionMall.Business.Business.Dto;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Roles;
using UnionMall.Roles.Dto;
using UnionMall.Web.Models.Roles;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize("SystemSet.ManagerRole")]
    [AbpMvcAuthorize]
    public class RoleController : UnionMallControllerBase
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly ICommonAppService _comService;
        public RoleController(IRoleAppService roleAppService, ICommonAppService comService
            , IBusinessAppService businessAppService)
        {
            _roleAppService = roleAppService;
            _comService = comService;
            _businessAppService = businessAppService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string Name = "", string BusinessId = "")
        {
            string where = string.Empty;
            where += _comService.GetWhere();
            if (!string.IsNullOrEmpty(Name))
                where += $" and Name like '%{Name}%'";
            if (!string.IsNullOrEmpty(BusinessId))
                where += $" and *.BusinessId = {BusinessId}";
            int total;

            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, "id desc", out total, where);
            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", page);
            }
            List<BusinessDropDownDto> businessdtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(businessdtoList, "Id", "BusinessName"));
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
            return View(page);
        }
        public async Task<ActionResult> Add(int? id)
        {
            var output = await _roleAppService.GetRoleForEdit(new EntityDto());
            var model = new EditRoleModalViewModel(output);

            if (id != null)
            {
                output = await _roleAppService.GetRoleForEdit(new EntityDto((int)id));
                model = new EditRoleModalViewModel(output);

            }
            List<BusinessDropDownDto> businessdtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", businessdtoList);
            return View("Add", model);
        }
    }
}