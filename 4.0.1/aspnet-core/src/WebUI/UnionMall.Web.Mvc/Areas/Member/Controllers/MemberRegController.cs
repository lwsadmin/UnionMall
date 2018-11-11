using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Controllers;
using UnionMall.Member;
using UnionMall.Entity;
namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("Member")]
    public class MemberRegController : UnionMallControllerBase
    {
        private readonly IMemberLevelAppService _levelAppService;

        public MemberRegController(IMemberLevelAppService levelAppService)
        {
            _levelAppService = levelAppService;
        }
        public async Task<IActionResult> Index()
        {
            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewBag.Level = levelDropDown;
            Entity.Member meber = new Entity.Member();

            return View(meber);
        }
    }
}