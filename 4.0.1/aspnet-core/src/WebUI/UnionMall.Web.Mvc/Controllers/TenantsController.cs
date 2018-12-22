using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using UnionMall.Authorization;
using UnionMall.Controllers;
using UnionMall.MultiTenancy;
using Abp.Runtime.Session;

namespace UnionMall.Web.Controllers
{
    [AbpMvcAuthorize]
    public class TenantsController : UnionMallControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly IAbpSession _AbpSession;
        public TenantsController(ITenantAppService tenantAppService, IAbpSession AbpSession)
        {
            _tenantAppService = tenantAppService;
            _AbpSession = AbpSession;
        }

        public async Task<ActionResult> Index()
        {
            var output = await _tenantAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); // Paging not implemented yet
            return View(output);
        }

        public async Task<ActionResult> EditTenantModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.Get(new EntityDto(tenantId));
            return View("_EditTenantModal", tenantDto);
        }
    }
}
