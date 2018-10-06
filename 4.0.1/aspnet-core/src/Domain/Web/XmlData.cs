using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace UnionMall.Core
{
    public class XmlData
    {
        public XmlDocument ProvincesXml
        {
            get
            {
                Type type = MethodBase.GetCurrentMethod().DeclaringType;

                string _namespace = type.Namespace;
                XmlDocument permitionXml = new XmlDocument();
                permitionXml.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnionMall.Localization.XmlData.address.Provinces.xml"));
                return permitionXml;
            }

        }

        public XmlDocument CitiesXml
        {
            get
            {
                XmlDocument permitionXml = new XmlDocument();
                permitionXml.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnionMall.Localization.XmlData.address.Cities.xml"));
                return permitionXml;
            }

        }

        public XmlDocument DistrictsXml
        {
            get
            {
                XmlDocument permitionXml = new XmlDocument();
                permitionXml.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnionMall.Localization.XmlData.address.Districts.xml"));
                return permitionXml;
            }

        }
    }
}
