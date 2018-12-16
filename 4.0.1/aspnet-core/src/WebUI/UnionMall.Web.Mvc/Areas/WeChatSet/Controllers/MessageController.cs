using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnionMall.Web.Mvc.Areas.WeChatSet.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}