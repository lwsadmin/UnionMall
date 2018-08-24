using Microsoft.AspNetCore.Antiforgery;
using UnionMall.Controllers;

namespace UnionMall.Web.Host.Controllers
{
    public class AntiForgeryController : UnionMallControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
