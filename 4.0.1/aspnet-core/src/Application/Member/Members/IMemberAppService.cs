using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Member.Dto;

namespace UnionMall.Member
{
    public interface IMemberAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");

        Task<JsonResult> SubmitRegAsync(RegDto dto);
    }
}
