using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using UnionMall.Sessions;

namespace UnionMall.Web.Views.Shared.Components.SideBarNav
{
    public class SideBarNavViewComponent : UnionMallViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;
        private readonly ISessionAppService _sessionAppService;

        public SideBarNavViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession, ISessionAppService sessionAppService)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
            _sessionAppService = sessionAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveMenuItemName = activeMenu
            };
            ViewBag.LoginInfo = await _sessionAppService.GetCurrentLoginInformations();
            return View(model);
        }
    }
}
