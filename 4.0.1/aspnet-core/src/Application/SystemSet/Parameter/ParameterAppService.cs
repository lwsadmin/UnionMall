using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.SystemSet
{
    public class ParameterAppService : ApplicationService, IParameterAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Parameter, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public ParameterAppService(ISqlExecuter sqlExecuter, IRepository<Parameter, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }

        public Task<Parameter> GetParameter(string key)
        {
            return _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId) ??
            _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0);
        }

        public void SaveParameter(Parameter p)
        {
            if (_Repository.FirstOrDefaultAsync(c => c.KeyName == p.KeyName && c.TenantId == (_AbpSession.TenantId ?? 0)) == null)
            {
                _Repository.Insert(p);
            }
            else
            {
                _Repository.Update(p);
            }
        }
    }
}
