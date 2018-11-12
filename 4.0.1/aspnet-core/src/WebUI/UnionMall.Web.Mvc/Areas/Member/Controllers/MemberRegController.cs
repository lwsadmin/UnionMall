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
using UnionMall.Member.Dto;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("Member")]
    public class MemberRegController : UnionMallControllerBase
    {
        private readonly IMemberLevelAppService _levelAppService;
        private readonly IMemberAppService _memberAppService;

        public MemberRegController(IMemberLevelAppService levelAppService, IMemberAppService memberAppService)
        {
            _levelAppService = levelAppService; _memberAppService = memberAppService;
        }
        public async Task<IActionResult> Index()
        {
            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewBag.Level = levelDropDown;


            return View();
        }
        //[HttpPost]
        //public JsonResult Save(RegDto dto)
        //{
        //    string msg = string.Empty;
        //    var booler = _memberAppService.SubmitReg(dto, out msg);
        //    if (msg == "Exist")
        //    {
        //        return Json(new { succ = booler, msg = L("Exist{0}", L("CardID")) + dto.CardID });
        //    }
        //    return Json(new { succ = booler, msg = L(msg) });
        //}
    }
}