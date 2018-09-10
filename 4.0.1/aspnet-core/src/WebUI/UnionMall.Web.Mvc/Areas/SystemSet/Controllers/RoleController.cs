using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.Roles;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
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
            string table = $"select *from tusers";
            if (_AbpSession.TenantId != null)
            {
                table += $" where TenantId={_AbpSession.TenantId}";
            }
            int total;
            DataSet ds = _roleAppService.GetRolePage(pageIndex, pageSize, table, "", out total);
            return View();
        }
    }
}