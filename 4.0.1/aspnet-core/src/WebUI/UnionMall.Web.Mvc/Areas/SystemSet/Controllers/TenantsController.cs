using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using UnionMall.MultiTenancy;

namespace UnionMall.Web.Mvc.Areas.SystemSet.Controllers
{
    [Area("SystemSet")]
    [AbpMvcAuthorize("SystemSet.Tenants")]
    public class TenantsController : UnionMallControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly IAbpSession _AbpSession;
        public TenantsController(ITenantAppService tenantAppService, IAbpSession AbpSession)
        {
            _tenantAppService = tenantAppService;
            _AbpSession = AbpSession;
        }
        public async Task<IActionResult> Index()
        {
            var tenantDto = await _tenantAppService.Get(new EntityDto((int)_AbpSession.TenantId));
            return View(tenantDto);
        }
    }
}