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

namespace UnionMall.Common.Attribute
{
    public class CommonAttributeAppService : ApplicationService, ICommonAttributeAppService
    {
        private readonly IRepository<CommonAttribute, long> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonAttributeAppService(IRepository<CommonAttribute, long> Repository, IAbpSession AbpSession,
            ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _sqlExecuter = sqlExecuter;
        }
        public async Task CreateOrEditAsync(CommonAttribute cat)
        {
            cat.TenantId = (int)AbpSession.TenantId;
            cat.Type = 0;
            if (cat.Id <= 0)
            {
                string sql = $@"select isnull(stuff((select  cast( ValueName as nvarchar(200))+',' from dbo.TCommonAttribute 
 where CategoryId={cat.CategoryId} for xml path('')),1,0,''),'')  ";
                string allValueName = _sqlExecuter.ExecuteDataSet(sql).Tables[0].Rows[0][0].ToString();
                for (int i = 1; i <= 10; i++)
                {
                    if (!allValueName.Contains($"Parameter{i}"))
                    {
                        cat.ValueName = $"Parameter{i}";
                        break;
                    }
                }
                await _Repository.InsertAsync(cat);
            }
            else
            {
                await _Repository.UpdateAsync(cat);
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

        public async Task<CommonAttribute> GetByIdAsync(long Id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<string> GetHtmlAttr(long categoryId, long goodsid = 0, int type = 0)
        {

            var query = await _Repository.GetAllListAsync(c => c.CategoryId == categoryId);
            if (query.Count == 0)
            {
                return "";
            }
            // Type t = msg.GetType();

            DataTable dtGoods = null;
            if (goodsid > 0 && type == 0)
            {
                string goodsSql = $@"select Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6,
 Parameter7, Parameter8, Parameter9, Parameter10 from TGoods where id={goodsid}";
                dtGoods = _sqlExecuter.ExecuteDataSet(goodsSql).Tables[0];

            }
            StringBuilder str = new StringBuilder();
            foreach (var item in query)
            {
                switch (item.DataType)
                {
                    case "Textbox":
                        str.Append($@"<div class='form-group'>
                                <label class='col-sm-2 control-label'>{item.Name}</label>
                                <div class='col-sm-5'><input type = 'text' name='{item.ValueName}' 
value='{(dtGoods == null ? "" : dtGoods.Rows[0][item.ValueName].ToString())}' class='form-control'></div>
                            </div>");
                        break;
                    case "Radio":
                        str.Append($@"<div class='form-group'>
                                <label class='col-sm-2 control-label'>{item.Name}</label>
                                <div class='col-sm-5'>");
                        string[] radio = item.Options.Split(',');
                        for (int i = 0; i < radio.Length; i++)
                        {
                            if (goodsid > 0)
                            {
                                str.Append($@"<div class='radio radio-inline radio-success'>
                                                <input type='radio' id ='{radio[i]}' value ='{radio[i]}' name='{item.ValueName}'
{(radio[i] == (dtGoods == null ? "" : dtGoods.Rows[0][item.ValueName].ToString()) ? "checked= 'checked'" : "")} >
                                                <label for='{radio[i]}'>{radio[i]}</label >
                                             </div> ");
                            }
                            else
                            {
                                str.Append($@"<div class='radio radio-inline radio-success'>
                                                <input type='radio' id='{radio[i]}' value ='{radio[i]}' name='{item.ValueName}' {(i == 0 ? "checked= ''" : "")} >
                                                <label for='{radio[i]}'>{radio[i]}</label >
                                             </div> ");
                            }

                        }
                        str.Append($@"</div>
                            </div>");
                        break;
                    case "Checkbox":
                        str.Append($@"<div class='form-group'>
                                <label class='col-sm-2 control-label'>{item.Name}</label>
                                <div class='col-sm-5'>");
                        string[] checkbox = item.Options.Split(',');
                        for (int i = 0; i < checkbox.Length; i++)
                        {
                            if (goodsid > 0)
                            {
                                str.Append($@"<div class='checkbox checkbox-inline checkbox-success'>
                                                <input type='checkbox' id ='{checkbox[i]}' value ='{checkbox[i]}' name ='{item.ValueName}' 
{(checkbox[i] == (dtGoods == null ? "" : dtGoods.Rows[0][item.ValueName].ToString()) ? "checked= 'checked'" : "")}>
                                                <label for='{checkbox[i]}' >{checkbox[i]}</label>
                                             </div> ");
                            }
                            else
                            {
                                str.Append($@"<div class='checkbox checkbox-inline checkbox-success'>
                                                <input type='checkbox' id ='{checkbox[i]}' value ='' name ='{item.ValueName}' checked= '' >
                                                <label for='{checkbox[i]}' >{checkbox[i]}</label>
                                             </div> ");
                            }

                        }
                        str.Append($@"</div>
                            </div>");
                        break;
                    case "TextArea":
                        str.Append($@"<div class='form-group'>
                                < label class='col-sm-2 control-label'>{item.Name}</label>
                                <div class='col-sm-5'><textarea name='{item.ValueName}'  class='form-control' cols='5'>{(dtGoods == null ? "" : dtGoods.Rows[0][item.ValueName].ToString())}</textarea></div>
                            </div>");
                        break;
                    default:
                        break;
                }
            }
            return str.ToString();
        }

        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select a.Id,a.Name,a.CategoryId, a.ValueName,a.DataType,a.DefaultValue,a.Options, c.Title from TCommonAttribute a left join TGoodsCategory c
on a.CategoryId=c.Id
where a.TenantId={AbpSession.TenantId}";
            }
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
