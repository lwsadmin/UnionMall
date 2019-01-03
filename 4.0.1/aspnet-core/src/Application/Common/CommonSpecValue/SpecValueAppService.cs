﻿using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Dto;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpec
{
    public class SpecValueAppService : ApplicationService, ISpecValueAppService
    {
        private readonly IRepository<Entity.CommonSpecValue, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public SpecValueAppService(IRepository<Entity.CommonSpecValue, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }

        public async Task AddOrEdit(CommonSpecValue value)
        {
            if (value.Id > 0)
            {
                await _Repository.UpdateAsync(value);
            }
            else
            {
                await _Repository.InsertAsync(value);
            }
        }

        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<List<CommonSpecValue>> GetBySpecId(long id)
        {
            return await _Repository.GetAllListAsync(c => c.SpecId == id);
        }

        public async Task<List<Entity.CommonSpecValue>> GetSelect()
        {
            var query = await _Repository.GetAllListAsync();
            return query;
        }
    }
}