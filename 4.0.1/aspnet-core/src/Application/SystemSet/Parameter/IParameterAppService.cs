using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;

namespace UnionMall.SystemSet
{
    public interface IParameterAppService : IApplicationService
    {
        Task<string> GetParameter(string key);
        void SaveParameter(Parameter p);
    }
}
