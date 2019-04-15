using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Controllers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using UnionMall.Common;
using Abp.Runtime.Session;

namespace UnionMall.Web.Mvc.Controllers
{
    public class CommonController : UnionMallControllerBase
    {
        private readonly ICommonAppService _AppService;
        private readonly IImageAppService _imgService;
        private readonly IAbpSession _AbpSession;
        public CommonController(ICommonAppService AppService, IImageAppService imgService, IAbpSession abpSession)
        {
            _AppService = AppService;
            _AbpSession = abpSession;
            _imgService = imgService;
        }
        public JsonResult Upload()
        {
            try
            {
                var fileBase = Request.Form.Files["File"];

                //if (fileBase == null || fileBase.FileName == "")
                //{
                //    return Json(new { success = false, url = "请选择要上传的图片!" });
                //}
                //if (fileBase.Length >= 1024 * 1000)
                //{
                //    return Json(new { success = false, url = "图片大小不能超过1M!" });
                //}
                //string ext = fileBase.FileName.Substring(fileBase.FileName.LastIndexOf('.'));
                //if (ext.ToLower() != ".jpg" && ext.ToLower() != ".jpeg" && ext.ToLower() != ".gif" && ext.ToLower() != ".png" && ext.ToLower() != ".bmp")
                //{
                //    return Json(new { success = false, url = "图片文件格式不正确!" });
                //}
                return _AppService.SaveSingleImg(fileBase, _AbpSession.TenantId != null ? (int)_AbpSession.TenantId : 0);
            }
            catch (Exception e)
            {
                Logger.Warn("----------------------------------");
                throw new Exception(e.StackTrace + e.Message);
            }


        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public ActionResult EditorUpload(string dir = "")
        {
            var fileBase = Request.Form.Files["imgFile"];

            return _AppService.SaveSingleImg(fileBase, _AbpSession.TenantId != null ? (int)_AbpSession.TenantId : 0);
        }

        public async Task DeleteImg(long key)
        {
            await _imgService.Delete(key);
            //string s = "";
        }
    }
}