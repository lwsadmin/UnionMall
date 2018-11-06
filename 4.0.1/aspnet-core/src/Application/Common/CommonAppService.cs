﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;
using UnionMall.MultiTenancy;

namespace UnionMall.Common
{
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IRepository<MultiTenancy.Tenant> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonAppService(IRepository<MultiTenancy.Tenant> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _sqlExecuter = sqlExecuter;
        }

        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public string GetWhere()
        {
            string where = string.Empty ;

            if (_AbpSession.TenantId == null || (int)_AbpSession.TenantId <= 0)
            {
                return where;
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and *.TenantId={_AbpSession.TenantId}";
            }

            DataTable role = _sqlExecuter.ExecuteDataSet($"select s.Id, r.Name RoleName,r.ManageRole,r.BusinessId from dbo.TUsers s " +
                $"left join dbo.TUserRoles ur on s.Id=ur.UserId left join dbo.TRoles r on ur.RoleId = r.Id where s.id={_AbpSession.UserId}").Tables[0];
            if (role.Rows[0]["RoleName"].ToString().ToUpper() != "ADMIN")// 
            {
                where += $" and *.BusinessId={role.Rows[0]["BusinessId"]}";
            }
            return where;


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
            string uploadFileName = DateTime.Now.ToString("MMddHHmmss") +
                DateTime.Now.Millisecond.ToString() +
                r.Next(1000, 9999).ToString() + Path.GetExtension(file.FileName);
            directoryPath += uploadFileName;
            using (FileStream fs = File.Create(directoryPath))
            {
                file.CopyTo(fs);
                fs.Flush();
                fs.Dispose();
            }
            //   FileStream fs = new FileStream(directoryPath, FileMode.Create);

            string url = path + uploadFileName;//返回的没有转换的相对路径到前端，前端传入后台存入数据库
            return new JsonResult(new { error = 0, succ = true, url = url });
        }
    }
}
