using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Entity;
using UnionMall.IRepositorySql;
using UnionMall.Member.Dto;

namespace UnionMall.Member
{
    public class MemberLevelAppService : ApplicationService, IMemberLevelAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;

        private readonly IRepository<MemberLevel, long> _Repository;
        public MemberLevelAppService(ISqlExecuter sqlExecuter, IRepository<MemberLevel, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.tenantid,m.title, cast(m.initpoint as float) initpoint,cast(m.sellprice as float) sellprice,
cast(m.minPoint as float) minPoint,cast(m.maxPoint as float) maxPoint from tmemberlevel m where 1=1 ";
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and m.TenantId={_AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task CreateOrEditAsync(MemberLevel dto)
        {
            dto.TenantId = _AbpSession.TenantId;
            if (dto.Id > 0)
            {
                await _Repository.UpdateAsync(dto);
                // return new JsonResult(new { succ = true, msg = "" });
            }
            else
            {
                await _Repository.InsertAsync(dto);
                //  return new JsonResult(new { succ = true, msg = "" });
            }

        }

        public async Task DeleteAsync(long Id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == Id);
            if (query != null)
            {
                await _Repository.DeleteAsync(query);
            }
        }
        public async Task<List<LevelDropDwonDto>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                query = query.FindAll(c => c.TenantId == (int)_AbpSession.TenantId);
            }
            if (query == null || query.Count == 0)
            {
                return new List<LevelDropDwonDto>();
            }
            return query.MapTo<List<LevelDropDwonDto>>();
        }

    }
}
