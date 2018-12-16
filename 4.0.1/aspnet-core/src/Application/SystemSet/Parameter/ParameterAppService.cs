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
using Abp.UI;
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

        public async Task<Parameter> GetParameter(string key)
        {
            var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
            if (s != null)
            {
                return s;
            }
            return (await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0));
        }

        public async Task SaveParameterValue(string key, string value)
        {
            var p = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == (_AbpSession.TenantId ?? 0));
            p.Value = value ?? "";
            await _Repository.UpdateAsync(p);
        }
        public async Task<string> GetParameterValue(string key)
        {
            var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
            if (s != null)
            {
                return s.Value;
            }
            return (await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0)).Value;
        }
        public async Task SaveParameter(Parameter p)
        {
            if (p.Id <= 0)
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
