using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.SystemSet
{
    public interface IWechatMessageAppService: IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        Task ChangeStatus(long id);
    }
}
