using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("Member")]
    public class CardCoreController : UnionMallControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}