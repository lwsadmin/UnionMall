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
using System.Data.Sql;
using System.Data.SqlClient;
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

        public IActionResult List()
        {

            if (_AbpSession.TenantId != null)
            {
                Where = $" TenantId={_AbpSession.TenantId}";
            }
            int pageIndex = 1;
            int pageSize = 15;
            string table = $"select *from tusers";
            int total;
            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "", out total);

            // Logger.Debug("-------------------");
            // Logger.Debug("-------------------");

            return View();
        }
    }
}
