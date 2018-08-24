using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using UnionMall.Controllers;

namespace UnionMall.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : UnionMallControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
