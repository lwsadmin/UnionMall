using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Controllers;
using UnionMall.Member;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;
using System.IO;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("UnionMember")]
    public class MemberController : UnionMallControllerBase
    {
        private readonly IMemberAppService _AppService;
        private readonly ICommonAppService _comAppService;
        private readonly IMemberLevelAppService _levelAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IChainStoreAppService _storeAppService;
        public MemberController(IMemberAppService AppService, IMemberLevelAppService levelAppService,
            IBusinessAppService businessAppService, IChainStoreAppService storeAppService, ICommonAppService comAppService)
        {
            _AppService = AppService;
            _levelAppService = levelAppService;
            _businessAppService = businessAppService;
            _storeAppService = storeAppService;
            _comAppService = comAppService;
        }

        [AbpMvcAuthorize]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string Level = "", string Name = "",
            string Business = "", string Store = "", string RegTimeFrom = "", string RegTimeTo = "")
        {

            string where = string.Empty; where += _comAppService.GetWhere();
            if (!string.IsNullOrEmpty(Level))
                where += $" and levelId={Level}";
            if (!string.IsNullOrEmpty(Name))
                where += $" and (FullName like'%{Name}%' or WeChatName like '%{Name}%')";
            if (!string.IsNullOrEmpty(Business))
                where += $" and BusinessId={Business}";
            if (!string.IsNullOrEmpty(Store))
                where += $" and ChainStoreId={Store}";
            if (!string.IsNullOrEmpty(RegTimeFrom))
                where += $" and RegTime>='{RegTimeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(RegTimeTo))
                where += $" and RegTime<='{RegTimeTo} 23:59:59'";

            ViewBag.PageSize = pageSize;
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }

            var businessDropDown = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new SelectList(businessDropDown, "Id", "BusinessName"));

            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewData.Add("Level", new SelectList(levelDropDown, "Id", "Title"));

            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));
            return View(pageList);
        }


        [IgnoreAntiforgeryToken]
        public ActionResult ImportExcel()
        {
            var fileBase = Request.Form.Files["File"];
            string msg = string.Empty; ;
            var json = _AppService.Import(fileBase);
            return json;
        }

        [IgnoreAntiforgeryToken]
        public FileResult ExportExcel(string Level = "", string Name = "",
            string Business = "", string Store = "", string RegTimeFrom = "", string RegTimeTo = "")
        {

            string where = string.Empty;
            where += _comAppService.GetWhere();
            if (!string.IsNullOrEmpty(Level))
                where += $" and levelId={Level}";
            if (!string.IsNullOrEmpty(Name))
                where += $" and (FullName like'%{Name}%' or WeChatName like '%{Name}%')";
            if (!string.IsNullOrEmpty(Business))
                where += $" and BusinessId={Business}";
            if (!string.IsNullOrEmpty(Store))
                where += $" and ChainStoreId={Store}";
            if (!string.IsNullOrEmpty(RegTimeFrom))
                where += $" and RegTime>='{RegTimeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(RegTimeTo))
                where += $" and RegTime<='{RegTimeTo} 23:59:59'";

            MemoryStream ms = _AppService.ExportToExcel(where);
            return File(ms.ToArray(), "application/vnd.ms-excel", "会员列表.xlsx");
        }
    }
}
