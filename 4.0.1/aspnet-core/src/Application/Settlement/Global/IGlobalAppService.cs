using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.Settlement
{
    public interface IGlobalAppService : IApplicationService
    {
        Task<Entity.GlobalSet> GetGlobalSet();
        void SaveGlobal(string column, decimal value);
    }
}
