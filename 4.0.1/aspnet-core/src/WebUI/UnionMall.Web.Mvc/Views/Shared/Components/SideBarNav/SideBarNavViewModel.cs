using Abp.Application.Navigation;

namespace UnionMall.Web.Views.Shared.Components.SideBarNav
{
    public class SideBarNavViewModel
    {
        public UserMenu MainMenu { get; set; }

        public string ActiveFisrtMenuItemName { get; set; }

        public string ActiveSecondMenuItemName { get; set; }
    }
}
