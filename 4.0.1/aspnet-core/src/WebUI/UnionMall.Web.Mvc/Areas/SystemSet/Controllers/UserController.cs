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
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Common;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("SystemSet.UserManager")]
    [Area("SystemSet")]
    public class UserController : UnionMallControllerBase
    {

        private readonly IRoleAppService _roleAppService;
        private readonly IUserAppService _userAppService;
        public readonly ILogger _logger;
        private readonly IBusinessAppService _AppService;
        private readonly IChainStoreAppService _storeAppService;
        private readonly ICommonAppService _comService;

        public UserController(IRoleAppService roleAppService, ICommonAppService comService, IBusinessAppService AppService,
            IUserAppService userAppService, IChainStoreAppService storeAppService, ILogger logger)
        {
            _roleAppService = roleAppService;
            _comService = comService;
            _userAppService = userAppService;
            _logger = logger;
            _AppService = AppService;
            _storeAppService = storeAppService;
        }
        public async Task<IActionResult> List(
            int page = 1, int pageSize = 10, string RoleId = "", string Name = "", string BusinessId = "")
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(Name))
                where += $" and s.UserName like '%{Name}%'";
            if (!string.IsNullOrEmpty(RoleId))
                where += $" and RoleId = {RoleId}";
            if (!string.IsNullOrEmpty(BusinessId))
                where += $" and r.BusinessId = {BusinessId}";
            int total;
            DataSet ds = _userAppService.GetUserPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);


            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<RoleDropDownDto> dtoList = (await _roleAppService.GetDropDown());
            ViewData.Add("Roles", new SelectList(dtoList, "Id", "DisplayName"));
            List<BusinessDropDownDto> businessdtoList = (await _AppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(businessdtoList, "Id", "BusinessName"));
            EditUserModalViewModel s = new EditUserModalViewModel();
            s.User = new UserDto();
            s.Roles = dtoList;
            ViewBag.EditUser = s;
            return View(pageList);
        }

        public async Task<ActionResult> Add(long? userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>((long)userId));
            List<RoleDropDownDto> dtoList = (await _roleAppService.GetDropDown());
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = dtoList
            };

            EditUserModalViewModel s = new EditUserModalViewModel();
            return View("_Add", model);
        }

        [IgnoreAntiforgeryToken]
        [HttpGet]
        public JsonResult DropDrowGetStore(long businessID)
        {
            List<StoreDropDownDto> s = _storeAppService.GetDropDown(businessID).Result;

            return Json(new { stores = s });
        }
    }
}
