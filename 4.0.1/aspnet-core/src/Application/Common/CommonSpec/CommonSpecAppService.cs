using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Attribute;
using UnionMall.Common.Dto;
using UnionMall.Entity;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpec
{
    public class CommonSpecAppService : ApplicationService, ICommonSpecAppService
    {
        private readonly IRepository<Entity.CommonSpec, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Entity.CommonSpecValue, long> _valueRepository;
        private readonly IRepository<Entity.CommonSpecObject, long> _objectRepository;
        public CommonSpecAppService(IRepository<Entity.CommonSpec, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter, IRepository<Entity.CommonSpecValue, long> valueRepository,
            IRepository<Entity.CommonSpecObject, long> objectRepository)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
            _valueRepository = valueRepository;
            _objectRepository = objectRepository;
        }
        public async Task CreateOrEditAsync(CreateOrEdit cat)
        {
            cat.Spec.TenantId = AbpSession.TenantId ?? 0;
            long id = 0;
            if (cat.Spec.Id > 0)
            {
                id = cat.Spec.Id;
                await _Repository.UpdateAsync(cat.Spec);
            }
            else
            {
                id = await _Repository.InsertAndGetIdAsync(cat.Spec);
            }
            if (cat.ValueList.Count > 0)
            {
                await _valueRepository.DeleteAsync(c => c.SpecId == id);
                foreach (var item in cat.ValueList)
                {
                    item.SpecId = id;
                    await _valueRepository.InsertAsync(item);
                }
            }

        }

        public async Task Delete(long id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _valueRepository.DeleteAsync(c => c.SpecId == id);
                await _Repository.DeleteAsync(query);
            }
        }

        public async Task<CreateOrEdit> GetByIdAsync(long Id)
        {
            CreateOrEdit s = new CreateOrEdit();
            s.Spec = await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
            s.ValueList = await _valueRepository.GetAllListAsync(c => c.SpecId == Id);
            return s;
        }

        public async Task<List<Entity.CommonSpec>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            //  query = query.FindAll(c => c.BusinessId == businessID);
            return query;
        }

        public async Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0)
        {
            StringBuilder sb = new StringBuilder();
            string sql = $@"select s.Id,s.Name,
stuff((select  cast(i.id as nvarchar)+'|', i.Text +','  from dbo.TCommonSpecValue i
 left join dbo.TCommonSpec f on i.SpecId=f.Id
 where s.Id=f.Id
for xml path('')),1,0,'') VName
from TCommonSpec s  
where s.CategoryId={categoryId}";
            DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                sb.Append($@"<div class='goodsTypeItem'>
                <b>{item["Name"]}：</b>");
                for (int i = 0; i < item["VName"].ToString().Split(',').Length; i++)
                {
                    if (item["VName"].ToString().Split(',')[i] == "")
                        continue;
                    sb.Append($@"&nbsp;<span class='simple_tag' data-specid='{item["Id"]}' data-objid='{item["VName"].ToString().Split(',')[i].Split('|')[0]}'
onclick=""select(this,'{item["Name"]}','{item["VName"].ToString().Split(',')[i].Split('|')[0]}');"">{item["VName"].ToString().Split(',')[i].Split('|')[1]}</span>");
                }
                sb.Append($@"</div>");
            }

            return sb.ToString();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select s.Id,s.Name,c.Title,s.Memo,
stuff((select  i.Text +','  from dbo.TCommonSpecValue i
 left join dbo.TCommonSpec f on i.SpecId=f.Id
 where s.Id=f.Id
for xml path('')),1,0,'') VName
from TCommonSpec s left join TGoodsCategory c on s.CategoryId=c.Id
where s.TenantId={AbpSession.TenantId} {where}  order by id OFFSET {(pageIndex - 1) * pageSize} ROW FETCH NEXT {pageSize} ROWS only";
            }
            string idSql = $@"select count(id) from TCommonSpec  where tenantid={AbpSession.TenantId} {where.Replace("s.", "")}";
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, "1", orderBy, out total, idSql, table);
        }

        Task<List<SpecDropDown>> ICommonSpecAppService.GetDropDown()
        {
            throw new NotImplementedException();
        }
    }
}
