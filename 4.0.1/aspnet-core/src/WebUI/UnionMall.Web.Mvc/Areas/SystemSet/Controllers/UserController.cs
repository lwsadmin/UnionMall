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
namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("SystemSet.UsersManager")]
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
        public async Task<IActionResult> List(int page = 1, int pageSize = 10,string RoleId="",
            string Name = "")
        {
            string table = $@"select s.id,s.UserName,s.Name,s.PhoneNumber,s.IsActive,s.CreationTime,s.LastLoginTime,
s.EmailAddress,ur.RoleId,r.DisplayName from TUsers s left join TUserRoles ur
on s.Id=ur.UserId left join TRoles r on ur.RoleId=r.Id where 1=1 ";
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                table += $" and s.TenantId={_AbpSession.TenantId}";
            }
            if (!string.IsNullOrEmpty(Name))
            { table += $" and s.UserName like '%{Name}%'"; }
            if (!string.IsNullOrEmpty(RoleId))
            { table += $" and RoleId = {RoleId}"; }
            int total;
            DataSet ds = _roleAppService.GetRolePage(page, pageSize, table, "id desc", out total);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<RoleDropDownDto> dtoList = (await _roleAppService.GetDropDown());
            ViewData.Add("Roles", new SelectList(dtoList, "Id", "DisplayName"));
            return View(pageList);
        }
    }
}
