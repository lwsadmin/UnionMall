using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UnionMall.IntegralNote
{
    public interface IIntegralNoteAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        DataSet GetStatisticsData(string where);
    }
}
