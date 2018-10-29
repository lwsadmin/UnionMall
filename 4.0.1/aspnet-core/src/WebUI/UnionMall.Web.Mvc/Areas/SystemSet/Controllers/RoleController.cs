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
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string Name = "")
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(Name))
                where += $" and Name like '%{Name}%'";
            int total;

            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, "id desc", out total, where);
            IPagedList page = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", page);
            }
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
            return View("Add", model);
        }
    }
}