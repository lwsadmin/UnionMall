using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Attribute;
using UnionMall.Common.Dto;
using UnionMall.Entity;
using UnionMall.Goods;
using UnionMall.IRepositorySql;

namespace UnionMall.Common.CommonSpec
{
    public class CommonSpecAppService : ApplicationService, ICommonSpecAppService
    {
        private readonly IRepository<Entity.CommonSpec, Guid> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<Entity.CommonSpecValue, Guid> _valueRepository;
        private readonly IRepository<Entity.CommonSpecObject, long> _objectRepository;
        private readonly IGoodsCategoryAppService _goodsCateAppServices;
        public CommonSpecAppService(IRepository<Entity.CommonSpec, Guid> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter, IRepository<Entity.CommonSpecValue, Guid> valueRepository,
            IRepository<Entity.CommonSpecObject, long> objectRepository, IGoodsCategoryAppService goodsCateAppServices)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
            _valueRepository = valueRepository;
            _objectRepository = objectRepository;
            _goodsCateAppServices = goodsCateAppServices;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
        public async Task CreateOrEditAsync(CreateOrEdit cat)
        {
            cat.Spec.TenantId = AbpSession.TenantId ?? 0;
            Guid id = Guid.Empty;
            if (cat.Spec.Id != Guid.Empty || cat.Spec.Id.ToString() == "")
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
                // await _valueRepository.DeleteAsync(c => c.SpecId == id);
                foreach (var item in cat.ValueList)
                {
                    item.SpecId = id;
                    if (item.Id == Guid.Empty)
                    {
                        await _valueRepository.InsertAsync(item);
                    }
                    else
                    {
                        var q = _valueRepository.Get(item.Id);
                        q.Text = item.Text;
                        await _valueRepository.UpdateAsync(q);

                    }

                }
            }

        }

        public async Task Delete(Guid id)
        {
            var query = _Repository.FirstOrDefault(c => c.Id == id);
            if (query != null)
            {
                await _valueRepository.DeleteAsync(c => c.SpecId == id);
                await _Repository.DeleteAsync(query);
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task<CreateOrEdit> GetByIdAsync(Guid Id)
        {
            CreateOrEdit s = new CreateOrEdit();
            s.Spec = await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
            s.ValueList = await _valueRepository.GetAllListAsync(c => c.SpecId == Id);
            return s;
        }
        [RemoteService(IsEnabled = false)]
        public async Task<List<Entity.CommonSpec>> GetDropDown()
        {
            var query = await _Repository.GetAllListAsync();
            //  query = query.FindAll(c => c.BusinessId == businessID);
            return query;
        }
        [RemoteService(IsEnabled = false)]
        public async Task<string> GetHtmlAttr(long categoryId, long goodsId, int type = 0)
        {

            StringBuilder sb = new StringBuilder();
            string sql = $@"select s.Id,s.Name,
stuff((select  cast(i.id as nvarchar(150))+'|', i.Text +','  from dbo.TCommonSpecValue i
 left join dbo.TCommonSpec f on i.SpecId=f.Id
 where s.Id=f.Id
for xml path('')),1,0,'') VName
from TCommonSpec s  
where s.CategoryId=";
            DataTable dt = _sqlExecuter.ExecuteDataSet(sql+ $"{categoryId}").Tables[0];
            if (dt == null || dt.Rows.Count == 0)
            {
                if (categoryId > 0)
                {
                    long parentid = (await _goodsCateAppServices.GetByIdAsync(categoryId)).ParentId;
                    dt = _sqlExecuter.ExecuteDataSet(sql + $"{parentid}").Tables[0];
                }
               
            }
            if (dt == null || dt.Rows.Count == 0)
                return sb.ToString();
            DataTable objTable = null;
            if (goodsId > 0)
            {
                string objSql = $@"select ValueText,Image,cast(Price as float) Price,cast(RetailPrice as float) RetailPrice,Stock,SKU from TCommonSpecObject where type=0 and ObjectId={goodsId}";
                objTable = _sqlExecuter.ExecuteDataSet(objSql).Tables[0];
            }
            string valueText = string.Empty;
            if (objTable != null && objTable.Rows.Count > 0)
            {
                foreach (DataRow itemvValueText in objTable.Rows)
                {
                    valueText += $@"{itemvValueText["ValueText"]}";
                }
            }
            string th = "";
            Dictionary<string, string> dicDataText = new Dictionary<string, string>();
            foreach (DataRow item in dt.Rows)
            {
                sb.Append($@"<div class='goodsTypeItem'>
                <b>{item["Name"]}：</b>");
                for (int i = 0; i < item["VName"].ToString().Split(',').Length; i++)
                {
                    if (item["VName"].ToString().Split(',')[i] == "")
                        continue;
                    dicDataText.Add(item["VName"].ToString().Split(',')[i].Split('|')[0], item["VName"].ToString().Split(',')[i].Split('|')[1]);
                    sb.Append($@"&nbsp;<span class='simple_tag {(valueText.Contains(item["VName"].ToString().Split(',')[i].Split('|')[0]) ? "active" : "")}' data-specid='{item["Id"]}' data-spec='{item["Name"]}' data-objid='{item["VName"].ToString().Split(',')[i].Split('|')[0]}'
onclick=""select(this,'{item["Name"]}','{item["VName"].ToString().Split(',')[i].Split('|')[0]}');"">{item["VName"].ToString().Split(',')[i].Split('|')[1]}</span>");
                }
                sb.Append($@"</div>");
                if (valueText.Contains(item["Id"].ToString()))
                {
                    th += $"<th>{item["Name"]}</th>";

                }

            }
            th += $"<th>{L("Price")}</th><th>{L("RetailPrice")}</th><th>{L("Count")}</th><th>SKU</th>";
            if (objTable == null || objTable.Rows.Count == 0)
            {
                th = "";
                sb.AppendFormat($@"<div class=""hr-line-dashed""></div><table class=""goodsLists table""><tbody></tbody></table> ");
                return sb.ToString();
            }
            sb.Append($@"<div class='hr-line-dashed'></div><table class='goodsLists table'><tbody><tr>{th}</tr>");

            if (objTable != null && objTable.Rows.Count > 0)
            {
                foreach (DataRow itemvValueText in objTable.Rows)
                {
                    //<td data-spec='' data-value=''>玫瑰金</td>
                    string[] array = itemvValueText["ValueText"].ToString().Split(",");
                    sb.Append($@"<tr>");
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] == "")
                            break;
                        sb.Append($@"<td data-spec='{array[i].Split(":")[0]}' 
data-value='{array[i].Split(":")[1]}'>{dicDataText.GetValueOrDefault(array[i].Split(":")[1])}</td>");
                    }

                    sb.Append($@"<td><input type='number' name='price' value='{itemvValueText["price"]}'></td>
<td><input type='number' name='RetailPrice' value='{itemvValueText["RetailPrice"]}'></td>
<td><input type='number' name='Stock' value='{itemvValueText["Stock"]}'></td>
<td><input type='text' name='SKU' value='{itemvValueText["SKU"]}'></td>
</tr>");
                }
            }
            sb.Append($@"</tbody></div>");

            return sb.ToString();
        }
        [RemoteService(IsEnabled = false)]
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
