using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;

namespace UnionMall.Web.Controllers
{
    [AbpMvcAuthorize]
    public class MainController : UnionMallControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}