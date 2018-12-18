using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Mvc.Areas.Statistics.Controllers
{
    [AbpMvcAuthorize]
    [AbpMvcAuthorize("StatisticsAnalysis.MemberConsumeStatistics")]
    [Area("Statistics")]
    public class MemberRechargeController : UnionMallControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}