using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Users;
using System.Data;
using UnionMall.Roles;
using Castle.Core.Logging;
using UnionMall.Roles.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Abp.Application.Services.Dto;
using UnionMall.Web.Models.Roles;
using UnionMall.Web.Models.Users;
using UnionMall.Users.Dto;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("SystemSet.UserManager")]
    [Area("SystemSet")]
    public class UserController : UnionMallControllerBase
    {

        private readonly IRoleAppService _roleAppService;
        private readonly IAbpSession _AbpSession;
        private readonly IUserAppService _userAppService;
        public readonly ILogger _logger;
        public UserController(IRoleAppService roleAppService, IAbpSession abpSession,
            IUserAppService userAppService, ILogger logger)
        {
            _roleAppService = roleAppService;
            _AbpSession = abpSession;
            _userAppService = userAppService;
            _logger = logger;
        }
        public async Task<IActionResult> List(
            int page = 1, int pageSize = 10, string RoleId = "", string Name = "")
        {
            string where = string.Empty;
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
                where += $" and s.TenantId={_AbpSession.TenantId}";
            if (!string.IsNullOrEmpty(Name))
                where += $" and s.UserName like '%{Name}%'";
            if (!string.IsNullOrEmpty(RoleId))
                where += $" and RoleId = {RoleId}";

            int total;
            DataSet ds = _userAppService.GetUserPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);


            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<RoleDropDownDto> dtoList = (await _roleAppService.GetDropDown());
            ViewData.Add("Roles", new SelectList(dtoList, "Id", "DisplayName"));

            EditUserModalViewModel s = new EditUserModalViewModel();
            s.User = new UserDto();
            s.Roles = (await _userAppService.GetRoles()).Items;
            ViewBag.EditUser = s;
            return View(pageList);
        }

        public async Task<ActionResult> Add(long? id)
        {
            var user = await _userAppService.Get(new EntityDto<long>((long)id));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_Add", model);
        }
    }
}
