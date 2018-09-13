using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services;

namespace UnionMall.Business.Buiness
{
    public interface IBusinessAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total);
    }
}
