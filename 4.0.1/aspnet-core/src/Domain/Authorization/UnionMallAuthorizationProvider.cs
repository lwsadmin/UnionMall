using System.IO;
using System.Reflection;
using System.Xml;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Hosting;

namespace UnionMall.Authorization
{
    public class UnionMallAuthorizationProvider : AuthorizationProvider
    {
        private readonly IHostingEnvironment _HostingEnvironment;
        public UnionMallAuthorizationProvider()
        {

        }
        public UnionMallAuthorizationProvider(IHostingEnvironment HostingEnvironment)
        {
            _HostingEnvironment = HostingEnvironment;
        }
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //系统默认设置权限
            //context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            //context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            //context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            XmlDocument NavigationXml = new XmlDocument();
            //string currentDirectory = Path.GetFullPath("../../Domain/Localization/XmlData/Navigation.xml");
            //string currentDirectory = Path.GetFullPath(_HostingEnvironment.WebRootPath + "/Navigation.xml");//该引用会引发异常
            //string[] name = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("UnionMall.Localization.XmlData.Navigation.xml");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true; //忽略注释
            XmlReader reader = XmlReader.Create(sm, settings);
            NavigationXml.Load(reader);
            XmlNodeList List = NavigationXml.SelectNodes("//Navigation//First");
            foreach (XmlNode item in List)
            {
                context.CreatePermission(item.Attributes["Name"].Value);
                if (item.ChildNodes != null && item.ChildNodes.Count > 0)
                {
                    foreach (XmlNode subItem in item.ChildNodes)
                    {
                        context.CreatePermission(item.Attributes["Name"].Value + "." + subItem.Attributes["Name"].Value);
                        if (subItem.ChildNodes.Count > 0)
                        {
                            foreach (XmlNode actionItem in subItem.ChildNodes)
                            {
                                string Name = item.Attributes["Name"].Value + "." +
                                     subItem.Attributes["Name"].Value + "." + actionItem.Attributes["Name"].Value;
                                context.CreatePermission(Name);
                            }
                        }
                    }
                }
            }

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, UnionMallConsts.LocalizationSourceName);
        }
    }
}
