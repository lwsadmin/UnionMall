using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.BalanceNote;
using UnionMall.Common;
using UnionMall.Controllers;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [Area("Statistics")]
    public class MemberBalanceController : UnionMallControllerBase
    {
        private readonly IBalanceNoteAppService _appService;
        private readonly ICommonAppService _comService;
        public MemberBalanceController(IBalanceNoteAppService appService, ICommonAppService comService)
        {
            _appService = appService;
            _comService = comService;
        }

        public async Task<IActionResult> List(string orderNumber, string way,
            string name, string cardid, string timeFrom, string timeTo, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(cardid))
                where += $" and cardid like '%{cardid}%'";
            if (!string.IsNullOrEmpty(way))
                where += $" and way ={way}";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and n.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and n.CreationTime<='{timeTo} 23:59:59'";
            int total;
            DataSet ds = _appService.GetPage(page, pageSize, "n.CreationTime desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            IList<SelectListItem> listItem = new List<SelectListItem>();
            Array values = System.Enum.GetValues(typeof(Entity.ConsumeType));
            foreach (int item in values)
            {
                listItem.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = L(System.Enum.GetName(typeof(Entity.ConsumeType), item))
                });
            }
            ViewBag.ConsumeType = listItem;
            return View(pageList);
        }

        public async Task<IActionResult> TableChat(string orderNumber, string way,
            string name, string cardid, string timeFrom, string timeTo)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(cardid))
                where += $" and cardid like '%{cardid}%'";
            if (!string.IsNullOrEmpty(way))
                where += $" and way ={way}";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and n.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and n.CreationTime<='{timeTo} 23:59:59'";
            DataTable addDt = _appService.GetStatisticsData(where + " and n.type=1").Tables[0];
            DataTable redDt = _appService.GetStatisticsData(where + " and n.type=0").Tables[0];
            string str = string.Empty;
            string addStr = string.Empty;
            string redStr = string.Empty;
            for (int i = 11; i >= 0; i--)
            {
                if (addDt.Select($" Time={DateTime.Now.AddMonths(-i).ToString("yyyyMM")}").Count() <= 0)
                    addStr += "0,";
                else
                    addStr += $"{addDt.Select($" Time={DateTime.Now.AddMonths(-i).ToString("yyyyMM")}")[0]["TotalValue"]},";

                if (redDt.Select($" Time={DateTime.Now.AddMonths(-i).ToString("yyyyMM")}").Count() <= 0)
                    redStr += "0,";
                else
                    redStr += $"{redDt.Select($" Time={DateTime.Now.AddMonths(-i).ToString("yyyyMM")}")[0]["TotalValue"]},";

                str += $"'{DateTime.Now.AddMonths(-i).ToString("yyyy-MM")}',";
            }
            ViewBag.Months = str;
            ViewBag.addStr = addStr;
            ViewBag.redStr = redStr;
            return View("_TableChat");
        }
    }
}