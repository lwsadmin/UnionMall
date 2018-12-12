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

        public async Task<string> GetParameter(string key)
        {
            var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
            if (s != null)
            {
                return s.Value;
            }
            return (await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0)).Value;
        }

        public async void SaveParameter(Parameter p)
        {
            if (_Repository.FirstOrDefaultAsync(c => c.KeyName == p.KeyName && c.TenantId == (_AbpSession.TenantId ?? 0)) == null)
            {
                await _Repository.InsertAsync(p);
            }
            else
            {
                await _Repository.UpdateAsync(p);
            }
        }
    }
}
