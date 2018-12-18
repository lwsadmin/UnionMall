using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.IntegralNote;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.MemberIntegralStatistics")]
    [Area("Statistics")]
    public class IntegralStatisticsController : UnionMallControllerBase
    {
        private readonly IIntegralNoteAppService _appService;
        private readonly ICommonAppService _comService;
        public IntegralStatisticsController(IIntegralNoteAppService appService, ICommonAppService comService)
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
            DataSet ds = _appService.GetPage(page, pageSize, "n.id desc", out total, where);
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
    }
}