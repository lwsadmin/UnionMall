using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services;

namespace UnionMall.Member.MemberLevel
{
    public interface IMemverLevelAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
    }
}
