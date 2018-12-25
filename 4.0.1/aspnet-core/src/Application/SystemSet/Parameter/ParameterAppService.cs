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
using Abp.Domain.Uow;

namespace UnionMall.SystemSet
{
    public class ParameterAppService : ApplicationService, IParameterAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Parameter, long> _Repository;
        public readonly IAbpSession _AbpSession;
        public readonly IUnitOfWorkManager _unitOfWorkManager;
        public ParameterAppService(ISqlExecuter sqlExecuter,
            IRepository<Parameter, long> Repository, IAbpSession AbpSession, IUnitOfWorkManager unitOfWorkManager)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<Parameter> GetParameter(string key)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
                if (s != null)
                {
                    return s;
                }
                return (await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0));
            }

        }

        public async Task SaveParameterValue(string key, string value)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var p = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
                if (p == null)
                {
                    p = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0);
                    Parameter newP = new Parameter();
                    newP.Title = p.Title;
                    newP.KeyName = p.KeyName;
                    newP.Value = value ?? "";
                    newP.TenantId = _AbpSession.TenantId;
                    newP.Memo = p.Memo;
                    await _Repository.InsertAsync(newP);
                }
                else
                {
                    p.Value = value ?? "";
                    await _Repository.UpdateAsync(p);
                }
            }

        }
        public async Task<string> GetParameterValue(string key)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == _AbpSession.TenantId);
                if (s != null)
                {
                    return s.Value;
                }
                return (await _Repository.FirstOrDefaultAsync(c => c.KeyName == key && c.TenantId == 0)).Value;
            }

        }
        public async Task SaveParameter(Parameter p)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == p.KeyName && c.TenantId == _AbpSession.TenantId);
                if (s == null)
                {
                    s = await _Repository.FirstOrDefaultAsync(c => c.KeyName == p.KeyName && c.TenantId == 0);
                    Parameter newP = new Parameter();
                    newP.Title = s.Title;
                    newP.KeyName = s.KeyName;
                    newP.Value = s.Value ?? "";
                    newP.TenantId = _AbpSession.TenantId;
                    newP.Memo = s.Memo;
                    await _Repository.InsertAsync(newP);
                }
                else
                {
                    s.Value = p.Value ?? "";
                    await _Repository.UpdateAsync(s);
                }
            }
        }


    }
}
