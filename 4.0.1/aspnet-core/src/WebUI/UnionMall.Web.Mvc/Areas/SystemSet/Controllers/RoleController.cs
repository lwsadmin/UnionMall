using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
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
        public RoleController(IRoleAppService roleAppService, IAbpSession abpSession)
        {
            _roleAppService = roleAppService;
            _AbpSession = abpSession;
        }
        public IActionResult List(int pageIndex = 1)
        {

            int pageSize = 15;
            string table = $"select r.Id,r.CreationTime,r.Description,r.DisplayName,r.IsDefault from TRoles r ";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            int total;

            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "id desc", out total);




            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);

            return View(page);
        }

        public async Task<ActionResult> Edit(int? roleId)
        {
            if (roleId != null)
            {
               
                var output = await _roleAppService.GetRoleForEdit(new EntityDto((int)roleId));
                var model = new EditRoleModalViewModel(output);
                return View("_Add", model);
            }
            else
            {
                return View("_Add", new EditRoleModalViewModel(new GetRoleForEditOutput()));
            }
            //  AjaxPager s = new AjaxPager();
        }
    }
}