using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.ConsumeNote;
using UnionMall.Controllers;
using UnionMall.Goods;
using UnionMall.Member;
using UnionMall.Statistics;

namespace UnionMall.Web.Controllers
{
    [AbpMvcAuthorize]
    public class MainController : UnionMallControllerBase
    {
        private readonly ICommonAppService _comService;
        private readonly IMemberLevelAppService _levelService;
        private readonly IMemberAppService _memService;
        private readonly IConsumeNoteAppService _consumeService;
        private readonly IWeChatPayNoteAppService _weiService;
        private readonly IAliPayNoteAppService _aliService;
        public MainController(ICommonAppService comService, IConsumeNoteAppService consumeService,
            IMemberLevelAppService levelService, IWeChatPayNoteAppService weiService,
            IAliPayNoteAppService aliService,
            IMemberAppService memService)
        {
            _comService = comService;
            _levelService = levelService;
            _memService = memService;
            _consumeService = consumeService;
            _weiService = weiService;
            _aliService = aliService;
        }
        public async Task<IActionResult> Index()
        {
            string comWhere = _comService.GetWhere();
            string where = " 1=1 " + comWhere;
            DataTable dt = await _levelService.GetIndexData(where);
            string m = string.Empty;

            foreach (DataRow item in dt.Rows)
            {
                m += "{name:'" + item["title"] + "',y:" + item["count"] + "},";
            }
            ViewBag.TotalMember = m;


            int total;
            ViewBag.Consume = _consumeService.GetPage(1, 40, "n.id desc", out total, comWhere).Tables[0];

            DataTable dtMember = await _memService.IndexMember(where);
            string M = string.Empty;
            string W = string.Empty;
            string date = string.Empty;
            for (int i = 15; i >= 1; i--)
            {
                if (dtMember.Select($" Day='{DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd")}'").Count() <= 0)
                {
                    M += $"0,";
                    W += $"0,";
                }
                else
                {
                    M += $"{dtMember.Select($" Day='{DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd")}'")[0]["M"]},";
                    W += $"{dtMember.Select($" Day='{DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd")}'")[0]["W"]},";
                }

                date += $"'{DateTime.Now.AddDays(-i).ToString("MM-dd")}',";
            }

            ViewBag.Data = date;
            ViewBag.M = M;
            ViewBag.W = W;


            DataTable dtWhat = _weiService.GetIndexData(comWhere +
                $" and CreationTime>='{DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd")}'" +
                $"  and CreationTime<='{DateTime.Now.ToString("yyyy-MM-dd")}'");
            DataTable dtAli = _aliService.GetIndexData(comWhere +
                $" and CreationTime>='{DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd")}'" +
                $"  and CreationTime<='{DateTime.Now.ToString("yyyy-MM-dd")}'");
            string WeChatLine = string.Empty; string AliLine = string.Empty;
            string dayStr = string.Empty;

            for (int i = 10; i >= 1; i--)
            {
                if (dtWhat.Select($" Time={DateTime.Now.AddDays(-i).ToString("yyyyMMdd")}").Count() <= 0)
                    WeChatLine += "0,";
                else
                    WeChatLine += $"{dtWhat.Select($" Time={DateTime.Now.AddDays(-i).ToString("yyyyMMdd")}")[0]["TotalValue"]},";

                if (dtAli.Select($" Time={DateTime.Now.AddDays(-i).ToString("yyyyMMdd")}").Count() <= 0)
                    AliLine += "0,";
                else
                    AliLine += $"{dtAli.Select($" Time={DateTime.Now.AddDays(-i).ToString("yyyyMMdd")}")[0]["TotalValue"]},";

                dayStr += $"'{DateTime.Now.AddDays(-i).ToString("MM-dd")}',";
            }
            ViewBag.ConDay = dayStr;
            ViewBag.WeChatLine = WeChatLine;
            ViewBag.AliLine = AliLine;

            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
    }
}