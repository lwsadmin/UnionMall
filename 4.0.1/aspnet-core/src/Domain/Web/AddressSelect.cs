using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UnionMall.Core
{
    public static class AddressSelect
    {

        public static string GetPro(string ProID="1", string NameAttr = "ProvinceID")
        {
            XmlDocument Xml = new XmlData().ProvincesXml;
            StringBuilder sb = new StringBuilder();
            //string path = HttpContext.Current.Server.MapPath("/Content/xmlData/address/Provinces.xml");
            //Xml.Load(path);
            XmlNodeList ProvinceNodeList = Xml.SelectNodes("//Province");
            StringBuilder strCity = new StringBuilder();
            sb.Append("<select  class='form-control' style='width:31%;float:left;' id='pro' name='" + NameAttr + "' onchange='ProChange(this)'>");
            foreach (XmlNode node in ProvinceNodeList)
            {
                if (node.Attributes["ID"].Value == ProID)
                {
                    sb.Append("<option selected='selected' value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["ProvinceName"].Value + "</option>");
                }
                else { sb.Append("<option value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["ProvinceName"].Value + "</option>"); }

            }
            sb.Append("</select>");
            return sb.ToString();
        }
        public static string GetCity(string ProID="1", string CityID = "1", string NameAttr = "CityID")
        {
            XmlDocument Xml = new XmlData().CitiesXml;
            //string path = HttpContext.Current.Server.MapPath("/Content/xmlData/address/Cities.xml");
            //Xml.Load(path);
            XmlNodeList CityNodeList = Xml.SelectNodes("//City[@PID='" + ProID + "']");
            StringBuilder strCity = new StringBuilder();
            strCity.Append("<select style='width:31%;float:left;margin-left:10px;' class='form-control' id='city' name='" + NameAttr + "' onchange='CityChange(this)'>");
            foreach (XmlNode node in CityNodeList)
            {
                if (node.Attributes["ID"].Value == CityID)
                {
                    strCity.Append("<option selected='selected' value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["CityName"].Value + "</option>");
                }
                else
                {
                    strCity.Append("<option value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["CityName"].Value + "</option>");
                }

            }
            strCity.Append("</select>");
            return strCity.ToString();
        }

        public static string GetDis(string CityID="1", string DisID = "1", string NameAttr = "DistrictID")
        {
            XmlDocument Xml = new XmlData().DistrictsXml;
            //string path = HttpContext.Current.Server.MapPath("/Content/xmlData/address/Districts.xml");
            //Xml.Load(path);
            XmlNodeList DisNodeList = Xml.SelectNodes("//District[@PID='" + CityID + "']");
            StringBuilder strCity = new StringBuilder();
            strCity.Append("<select style='width:31%;float:left;margin-left:10px;' class='form-control' id='dis' name='" + NameAttr + "'>");
            foreach (XmlNode node in DisNodeList)
            {
                if (node.Attributes["ID"].Value == DisID)
                {
                    strCity.Append("<option selected='selected' value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["DistrictName"].Value + "</option>");

                }
                else { strCity.Append("<option value='" + node.Attributes["ID"].Value + "'>" + node.Attributes["DistrictName"].Value + "</option>"); }

            }
            strCity.Append("</select>");
            return strCity.ToString();
        }

        public static HtmlString GetAddress<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> ProvinceID, Expression<Func<TModel, TProperty>> CityID, Expression<Func<TModel, TProperty>> DistrictID, Object HtmlAttribute)
        {
            StringBuilder sb = new StringBuilder();
            string ProName = string.Empty; string CityName = string.Empty; string DisName = string.Empty;
            var param = Expression.Parameter(typeof(TModel), "ProviceID");



            string ProValue = GetLamdaValue<TModel, TProperty>(htmlHelper, ProvinceID, out ProName);
            string CityValue = GetLamdaValue<TModel, TProperty>(htmlHelper, CityID, out CityName);
            string DisValue = GetLamdaValue<TModel, TProperty>(htmlHelper, DistrictID, out DisName);

            string s = ProvinceID.Name;
            string v = ProvinceID.Parameters[0].Name;
            sb.Append(GetPro());
            sb.Append(GetCity());
            sb.Append(GetDis());
            return new HtmlString(sb.ToString());
        }

        public static string GetLamdaValue<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> ID, out string name)
        {
            string lambda = ID.Body.ToString();//形如Model.ProviceID
            name = "";
            string value = string.Empty;
            if (htmlHelper.ViewData.Model != null)
            {

                //name = ModelMetadataProviderExtensions.FromLambdaExpression(ID, htmlHelper.ViewData).PropertyName.ToString();
                //  value = ModelMetadata.FromLambdaExpression(ID, htmlHelper.ViewData).Model.ToString();
                //name= ModelMetadataProviderExtensions.

            }
            return value;
        }
    }
}
