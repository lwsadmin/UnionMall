using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using UnionMall.Controllers;

namespace UnionMall.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : UnionMallControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
