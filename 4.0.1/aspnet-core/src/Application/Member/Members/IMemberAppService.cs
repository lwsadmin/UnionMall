using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Member.Dto;

namespace UnionMall.Member
{
    public interface IMemberAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");

        Task<Entity.Member> GetEntity(long id);
        Task<Entity.Member> GetEntity(string cardId);
        Task<CardCoreDto> GetCardCore(string cardId);
        Task<JsonResult> SubmitRegAsync(RegDto dto);
        Task<JsonResult> MemberRecharge(Entity.RechargeNote note);
        Task<JsonResult> Import(IFormFile flie);
        Task<MemoryStream> ExportToExcel(string where);
    }
}
