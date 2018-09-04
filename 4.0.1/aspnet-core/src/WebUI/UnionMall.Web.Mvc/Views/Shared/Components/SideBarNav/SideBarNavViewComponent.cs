using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using UnionMall.Sessions;
using UnionMall.Roles;
using UnionMall.Sessions.Dto;

namespace UnionMall.Web.Views.Shared.Components.SideBarNav
{
    public class SideBarNavViewComponent : UnionMallViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;
        private readonly ISessionAppService _sessionAppService;
        private readonly IRoleAppService _roleAppService;

        public SideBarNavViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession, ISessionAppService sessionAppService, IRoleAppService roleAppService)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
            _sessionAppService = sessionAppService;
            _roleAppService = roleAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string FirstMenu = "", string SecondMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveFisrtMenuItemName = FirstMenu,
                ActiveSecondMenuItemName = SecondMenu
            };

            GetCurrentLoginInformationsOutput s = await _sessionAppService.GetCurrentLoginInformations();
            ViewBag.LoginInfo = s;
           // ViewBag.Role = await _roleAppService.GetRole(s.User.Id);
            return View(model);
        }
    }
}
