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
        Task<Parameter> GetParameter(string key);
       // void SaveParameter(string key, string value);
        Task SaveParameter(Parameter p);
    }
}
