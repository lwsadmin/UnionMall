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
using UnionMall.Controllers;
using UnionMall.Roles;
using UnionMall.Roles.Dto;
using UnionMall.Web.Models.Roles;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize]
    public class RoleController : UnionMallControllerBase
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IAbpSession _AbpSession;
        private readonly IBusinessAppService _businessAppService;
        public RoleController(IRoleAppService roleAppService, IAbpSession abpSession
            , IBusinessAppService businessAppService)
        {
            _roleAppService = roleAppService;
            _AbpSession = abpSession;
            _businessAppService = businessAppService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, string Name = "")
        {

            int pageSize = 15;
            string table = $"select r.Id,r.CreationTime,r.Description,r.DisplayName,r.Name,r.IsDefault from TRoles r where IsDeleted=0 ";
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId >= 0)
            {
                table += $" and TenantId={_AbpSession.TenantId}";
            }

            if (!string.IsNullOrEmpty(Name))
            {
                table += $" and Name like'%{Name}%'";
            }
            int total;

            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "id desc", out total);
            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", page);
            }
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
           // ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            return View(page);
        }

        public async Task<ActionResult> Edit(int? roleId)
        {
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            if (roleId == null)
            {
                RoleDto dto = new RoleDto();
                return View("_Add", dto);
            }
            else
            {
                return View("_Add", new EditRoleModalViewModel(new GetRoleForEditOutput()));
            }
            //  AjaxPager s = new AjaxPager();
        }
        public async Task<ActionResult> Add(int? id)
        {
            // var permissions = (await _roleAppService.GetAllPermissions()).Items;
            if (id == null)
            {
                var output = await _roleAppService.GetRoleForEdit(new EntityDto());
                var model = new EditRoleModalViewModel(output);
                return View("Add", model);
            }
            else
            {
                var output = await _roleAppService.GetRoleForEdit(new EntityDto((int)id));
                var model = new EditRoleModalViewModel(output);
                return View("Add", model);
            }

        }
    }
}