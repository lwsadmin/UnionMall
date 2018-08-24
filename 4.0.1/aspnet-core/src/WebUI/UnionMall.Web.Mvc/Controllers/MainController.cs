using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Controllers
{
    public class MainController : UnionMallControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}