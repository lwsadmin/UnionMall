using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
<<<<<<< HEAD
using UnionMall.Users;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using UnionMall.Roles;
=======
using Castle.Core.Logging;
using UnionMall.Users;
>>>>>>> 674e929945342addb72b79a4ec3f1985f957beb3

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("UserManager")]
    [Area("SystemSet")]
    public class UserController : UnionMallControllerBase
    {
<<<<<<< HEAD
        private readonly IRoleAppService _roleAppService;
        private readonly IAbpSession _AbpSession;
        public UserController(IRoleAppService roleAppService, IAbpSession abpSession)
        {
            _roleAppService = roleAppService;
            _AbpSession = abpSession;
=======
        private readonly IUserAppService _userAppService;
        public readonly ILogger _logger;
        public UserController(IUserAppService userAppService, ILogger logger)
        {
            _userAppService = userAppService;
            _logger = logger;
>>>>>>> 674e929945342addb72b79a4ec3f1985f957beb3
        }

        public IActionResult List()
        {
<<<<<<< HEAD
            if (_AbpSession.TenantId != null)
            {
                Where = $" TenantId={_AbpSession.TenantId}";
            }
            int pageIndex = 1;
            int pageSize = 15;
            string table = $"select *from tusers";
            int total;
            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "", out total);
=======
            Logger.Debug("-------------------");
>>>>>>> 674e929945342addb72b79a4ec3f1985f957beb3
            return View();
        }
    }
}