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

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("SystemSet.UsersManager")]
    [Area("SystemSet")]
    public class UserController : UnionMallControllerBase
    {

        private readonly IRoleAppService _roleAppService;
        private readonly IAbpSession _AbpSession;
        public UserController(IRoleAppService roleAppService, IAbpSession abpSession)
        {
            _roleAppService = roleAppService;
            _AbpSession = abpSession;
        }

        private readonly IUserAppService _userAppService;
        public readonly ILogger _logger;
        public UserController(IUserAppService userAppService, ILogger logger)
        {
            _userAppService = userAppService;
            _logger = logger;

        }

        public IActionResult List(int pageIndex = 1)
        {   
            int pageSize = 15;
            string table = $@"select s.id,s.UserName,s.Name,s.PhoneNumber,s.IsActive,s.CreationTime,s.LastLoginTime,
s.EmailAddress from TUsers s ";
            if (_AbpSession.TenantId != null)
            {
                table += $" where s.TenantId={_AbpSession.TenantId}";
            }
            int total;
            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "id desc", out total);
           
            return View(ds.Tables[0]);
        }
    }
}
