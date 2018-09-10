﻿using System;
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

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [AbpMvcAuthorize("UserManager")]
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
            return View();
        }
    }
}