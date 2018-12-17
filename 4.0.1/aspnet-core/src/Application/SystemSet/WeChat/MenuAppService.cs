using Abp.Application.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.SystemSet.WeChat
{
    public class MenuAppService : ApplicationService, IMenuAppService
    {
        public MenuError Create(string jsonData)
        {
            MenuError error = new MenuError();
            string access_token = "";
            string ResponseUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            HttpContent httpContent = new StringContent(jsonData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(ResponseUrl, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;
                error = JsonConvert.DeserializeObject<MenuError>(s);
            }
            return error;
        }

        public Menu GetMenu(string ACCESS_TOKEN)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", ACCESS_TOKEN);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";//设置请求的方法
            request.Accept = "*/*";//设置Accept标头的值
            string responseStr = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }
            }
            var j = JsonConvert.DeserializeObject<JObject>(responseStr);
            var menu = j["menu"].ToString();
            return JsonConvert.DeserializeObject<Menu>(menu);
        }
    }
}
