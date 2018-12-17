using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;
using UnionMall.ConsumeNote;
using UnionMall.Controllers;
using X.PagedList;
namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.MemberConsumeStatistics")]
    [Area("Statistics")]
    public class MemberConsumeController : UnionMallControllerBase
    {
        private readonly IConsumeNoteAppService _appService;
        private readonly ICommonAppService _comService;
        public MemberConsumeController(IConsumeNoteAppService appService, ICommonAppService comService)
        {
            _appService = appService;
            _comService = comService;
        }

        public async Task<IActionResult> List(string orderNumber,
            string name, string type, string timeFrom, string timeTo, int page = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(type))
                where += $" and type ={type}";
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

        public async Task<IActionResult> TableChat(string orderNumber, string name, string type, string timeFrom, string timeTo)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(orderNumber))
                where += $" and billNumber like '%{orderNumber}%'";
            if (!string.IsNullOrEmpty(name))
                where += $" and Name like '%{name}%'";
            if (!string.IsNullOrEmpty(type))
                where += $" and type ={type}";
            if (!string.IsNullOrEmpty(timeFrom))
                where += $" and n.CreationTime>='{timeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(timeTo))
                where += $" and n.CreationTime<='{timeTo} 23:59:59'";

            DataTable dt = _appService.GetTotalData(where).Tables[0];
            Array values = System.Enum.GetValues(typeof(Entity.ConsumeType));
            string str = "";
            foreach (int item in values)
            {
                var f = "0";
                if (dt.Select($" type={item}").Count() > 0)
                {
                    f = dt.Select($" type={item}")[0]["TotalPaid"].ToString();
                }
                str += $"['{L(System.Enum.GetName(typeof(Entity.ConsumeType), item))}',{f}],";
            }
            ViewBag.Str = str;
            return View("_TableChat");

        }
    }
}