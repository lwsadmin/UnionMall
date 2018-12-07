using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnionMall.Web.Mvc.Areas.MobileTerminal.Controllers
{
    public class PaySetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}