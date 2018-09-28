using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace UnionMall.Web.Mvc.Controllers
{
    public class CommonController : UnionMallControllerBase
    {
        public JsonResult Upload()
        {
            var fileBase = Request.Form.Files["File"];
            
            string url = "";

            if (fileBase == null || fileBase.FileName == "")
            {
                return Json(new { success = false, url = "请选择要上传的图片!" });
            }
            if (fileBase.Length >= 1024 * 1000)
            {
                return Json(new { success = false, url = "图片大小不能超过1M!" });
            }
            string ext = fileBase.FileName.Substring(fileBase.FileName.LastIndexOf('.'));
            if (ext.ToLower() != ".jpg" && ext.ToLower() != ".jpeg" && ext.ToLower() != ".gif" && ext.ToLower() != ".png" && ext.ToLower() != ".bmp")
            {
                return Json(new { success = false, url = "图片文件格式不正确!" });
            }
          //  bool f = CommonLogic.SaveSingleImg(fileBase, this.OperatorGuid, out url);
            return Json(new { success = false, url = url, size = fileBase.Length, name = fileBase.FileName });
        }
    }
}