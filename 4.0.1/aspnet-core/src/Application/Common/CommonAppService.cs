using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnionMall.MultiTenancy;

namespace UnionMall.Common
{
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IRepository<MultiTenancy.Tenant> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        public CommonAppService(IRepository<MultiTenancy.Tenant> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
        }
        public JsonResult SaveSingleImg(IFormFile file, int tenandId)
        {
            Tenant t = _Repository.Get(tenandId);

            string path = string.Format("/Upload/{0}/Images/{1}/", t.TenancyName, DateTime.Now.Year.ToString());
            string directoryPath = _HostingEnvironment.WebRootPath + path;

            //WebRootPath,带wwwroot

            //string directoryPath = _HostingEnvironment.ContentRootPath + path;
            //不带wwwroot
            if (!Directory.Exists(directoryPath))//不存在这个文件夹就创建这个文件夹  
            {
                Directory.CreateDirectory(directoryPath);
            }
            Random r = new Random(Guid.NewGuid().GetHashCode());
            string uploadFileName = DateTime.Now.ToString("MMddhhmmss") +
                DateTime.Now.Millisecond.ToString() +
                r.Next(1000, 9999).ToString() + Path.GetExtension(file.FileName);
            directoryPath += uploadFileName;
            using (FileStream fs = File.Create(directoryPath))
            {
                file.CopyTo(fs);
                fs.Flush();
                //fs.Dispose();
            }
         //   FileStream fs = new FileStream(directoryPath, FileMode.Create);
          
            string url = path + uploadFileName;//返回的没有转换的相对路径到前端，前端传入后台存入数据库
            return new JsonResult(new { succ = true, url = url });
        }
    }
}
