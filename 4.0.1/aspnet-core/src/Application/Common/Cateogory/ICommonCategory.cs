using System;
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
using Abp.UI;
using UnionMall.Entity;
using UnionMall.Common.Cateogory.Dto;

namespace UnionMall.Common
{

    public interface ICommonCategoryAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task CreateOrEditAsync(CommonCategory cat);
        Task<CommonCategory> GetByIdAsync(long Id);
        Task Delete(long id);
        Task<List<CommonCategory>> GetAllListByParentIdAsync(long parentId);
        List<CatDropDownDto> GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0);
    }
}
