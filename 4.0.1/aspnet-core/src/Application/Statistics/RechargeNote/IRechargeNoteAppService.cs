using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Statistics
{
    public interface IRechargeNoteAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
        DataSet GetTotalData(string where);
        Task Create(Entity.RechargeNote note);
    }
}
