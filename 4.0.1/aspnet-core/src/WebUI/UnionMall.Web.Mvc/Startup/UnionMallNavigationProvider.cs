using System.IO;
using System.Xml;
using Abp.Application.Navigation;
using Abp.Localization;
using UnionMall.Authorization;

namespace UnionMall.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class UnionMallNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {

            XmlDocument NavigationXml = new XmlDocument();

            string currentDirectory = Path.GetFullPath("../../Domain/Localization/XmlData/Navigation.xml");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //忽略注释
            XmlReader reader = XmlReader.Create(currentDirectory, settings);

            NavigationXml.Load(reader);
            XmlNodeList List = NavigationXml.SelectNodes("//Navigation//First");
            if (List != null)
            {
                foreach (XmlNode item in List)
                {
                    MenuItemDefinition first = new MenuItemDefinition(
                        item.Attributes["Name"].Value,
                        L(item.Attributes["Name"].Value),
                        url: "",
                        icon: item.Attributes["Icon"].Value,
                        requiredPermissionName: item.Attributes["Name"].Value,
                        requiresAuthentication:true
                        );

                    if (item.ChildNodes != null && item.ChildNodes.Count > 0)
                    {
                        foreach (XmlNode subItem in item.ChildNodes)
                        {
                            MenuItemDefinition second = new MenuItemDefinition(
                                 subItem.Attributes["Name"].Value,
                                 L(subItem.Attributes["Name"].Value),
                                 url: subItem.Attributes["Url"].Value,
                                 icon: "",
                                 requiredPermissionName: item.Attributes["Name"].Value + "." + subItem.Attributes["Name"].Value
                                 );
                            first.AddItem(second);
                        }
                    }
                    context.Manager.MainMenu.AddItem(first);
                }
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UnionMallConsts.LocalizationSourceName);
        }
    }
}
