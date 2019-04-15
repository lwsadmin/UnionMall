using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using UnionMall.Controllers;
using UnionMall.Goods;

namespace UnionMall.Web.Controllers
{
 
    public class HomeController : UnionMallControllerBase
    {
        private readonly IBrandAppService _AppService;
        public HomeController(IBrandAppService AppService)
        {
            _AppService = AppService;
        }
        public ActionResult Index()
        {
            _AppService.GetMultiSelect();
            return View();
        }
	}
}
