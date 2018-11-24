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
using UnionMall.IRepositorySql;
using UnionMall.Entity;
using UnionMall.Member.Dto;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using UnionMall.Common;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
//using UnionMall.SystemSet;

namespace UnionMall.Member
{
    public class MemberAppService : ApplicationService, IMemberAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IAbpSession _AbpSession;
        private readonly ICommonAppService _comServices;
        private readonly IRepository<UnionMall.Entity.Member, long> _Repository;
        // private readonly ILogAppService _log;
        public MemberAppService(ISqlExecuter sqlExecuter, IRepository<UnionMall.Entity.Member, long> Repository,
            IAbpSession AbpSession, ICommonAppService comServices
            //, ILogAppService log
            )
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _comServices = comServices;
            // _log = log;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.FullName,stuff(m.WechatName,2,1,'*') WechatName,m.TenantId,m.levelId,m.HeadImg,m.Sex,stuff(m.CardID,8,4,'****') CardID,
stuff(m.Mobile,8,4,'****') Mobile,m.Balance,m.Integral,
convert(nvarchar(100),m.RegTime,20) RegTime,m.businessId,m.chainStoreId,l.Title,b.BusinessName,c.Name StoreName from dbo.TMember m
left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TBusiness b on
m.businessId=b.Id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";
            }
            table += where.Replace("*", "m");
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }

        public async Task CreateOrEditAsync(UnionMall.Entity.Member dto)
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

        public async Task<JsonResult> SubmitRegAsync(RegDto dto)
        {
            try
            {
                dto.TenantId = _AbpSession.TenantId;
                var entity = _Repository.FirstOrDefault(c => c.CardID == dto.CardID && c.TenantId == dto.TenantId);
                LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
                if (entity != null)
                {
                    return new JsonResult(new { succ = false, msg = L("Exist{0}", L("CardID")) + dto.CardID });
                }
                string sql = $@" select s.chainStoreId,c.BusinessId from dbo.TUsers s  left join dbo.TChainStore c
  on s.chainStoreId = c.Id  where s.id = {_AbpSession.UserId}";
                DataTable t = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
                dto.BusinessId = (long)t.Rows[0]["businessId"];
                dto.ChainStoreId = (long)t.Rows[0]["ChainStoreId"];

                await _Repository.InsertAsync(dto.MapTo<UnionMall.Entity.Member>());
            }
            catch (Exception e)
            {

                throw new UserFriendlyException(e.Message);
            }

            return new JsonResult(new { succ = true });

        }
        [Abp.Domain.Uow.UnitOfWork]
        public JsonResult Import(IFormFile flie)
        {

            string msg = string.Empty;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
            DataTable dt = _comServices.ExcelToDataTable(flie, out msg);
            if (dt == null || dt.Rows.Count == 0)
            {
                return new JsonResult(new { succ = false, msg = msg });
            }
            bool CoulmnCheck = true; string coulmnMsg = string.Empty;
            if (!dt.Columns.Contains("会员卡号"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【会员卡号】");
            }
            if (!dt.Columns.Contains("微信名"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【微信名】");
            }
            if (!dt.Columns.Contains("姓名"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【姓名】");
            }
            if (!dt.Columns.Contains("会员等级"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【会员等级】");
            }
            if (!dt.Columns.Contains("归属门店"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【归属门店】");
            }
            if (!dt.Columns.Contains("性别"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【性别】");
            }
            if (!dt.Columns.Contains("可用积分"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【可用积分】");
            }
            if (!dt.Columns.Contains("可用余额"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【可用余额】");
            }
            if (!dt.Columns.Contains("推荐人卡号"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【推荐人卡号】");
            }
            if (!dt.Columns.Contains("详细地址"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【详细地址】");
            }
            if (!dt.Columns.Contains("电子邮件"))
            {
                CoulmnCheck = false; coulmnMsg += L("NotExist{0}", "【电子邮件】");
            }
            if (CoulmnCheck == false)
            {
                return new JsonResult(new { succ = false, msg = coulmnMsg });
            }
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Entity.Member member = new Entity.Member();
                    member.TenantId = _AbpSession.TenantId;
                    member.CardID = row["会员卡号"].ToString();
                    member.FullName = row["姓名"].ToString();
                    member.WeChatName = row["微信名"].ToString();
                    if (member.WeChatName == "")
                    {
                        member.WeChatName = member.FullName;
                    }
                    DataTable levelTable = _sqlExecuter.ExecuteDataSet($"select id from TMemberLevel l where l.TenantId={_AbpSession.TenantId}" +
                         $" and l.Title='{row["会员等级"]}'").Tables[0];
                    if (levelTable.Rows.Count == 0)
                    {
                        msg += L("NotExist{0}", row["会员等级"].ToString());
                        continue;
                    }

                    member.LevelId = (long)levelTable.Rows[0][0];

                    DataTable storeTable = _sqlExecuter.ExecuteDataSet($"select id,BusinessId from TChainStore c where c.TenantId={_AbpSession.TenantId}" +
            $" and c.Name='{row["归属门店"]}'").Tables[0];
                    if (levelTable.Rows.Count == 0)
                    {
                        msg += L("NotExist{0}", row["归属门店"].ToString());
                        continue;
                    }
                    member.ChainStoreId = (long)storeTable.Rows[0]["id"];
                    member.BusinessId = (long)storeTable.Rows[0]["BusinessId"];
                    if (row["性别"].ToString() == "男")
                        member.Sex = 0;
                    else
                        member.Sex = 1;

                    member.Integral = Convert.ToDecimal(row["可用积分"]);
                    member.Balance = Convert.ToDecimal(row["可用余额"]);
                    member.Mobile = row["手机号"].ToString();
                    member.PassWord = "123";
                    if (row["推荐人卡号"] != null && row["推荐人卡号"].ToString().Trim() != "")
                    {
                        var m = _Repository.FirstOrDefault(c => c.CardID == row["推荐人卡号"].ToString() && c.TenantId == _AbpSession.TenantId);
                        member.ReferrerID = m.Id;
                    }
                    member.RegTime = DateTime.Now;
                    member.Address = row["详细地址"].ToString();
                    member.Email = row["电子邮件"].ToString();
                    _Repository.Insert(member);

                }
                return new JsonResult(new { succ = true, msg = msg });
            }
            catch (Exception ex)
            {
                Logger.Warn("---------------------------" + ex.StackTrace);
                return new JsonResult(new { succ = false, msg = ex.Message });
            }
        }

        public MemoryStream ExportToExcel(string where)
        {
            string sql = $@"select  m.FullName 姓名,stuff(m.WechatName,2,1,'*') 微信名,
 case m.Sex  when 0 then '男' else '女' end 性别, stuff(m.CardID,8,4,'****') 卡号,stuff(m.Mobile,8,4,'****') 手机号,m.Balance 余额
,m.Integral 积分,convert(nvarchar(100),m.RegTime,20)  注册时间,l.Title 等级,b.BusinessName 所属商户,
c.Name  所属门店 from dbo.TMember m left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TBusiness b on
m.businessId=b.Id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";

            sql += where.Replace("*", "m");
            int total;
            string idSql = $@"select count( m.id) from dbo.tmember m left join dbo.tmemberlevel l on m.levelid=l.id left join dbo.tbusiness b on
m.businessid = b.id left join dbo.tchainstore c on m.chainstoreid = c.id where 1 = 1" + where.Replace("*", "m");
            DataTable dt = _sqlExecuter.GetPagedList(1, int.MaxValue, sql, " m.RegTime desc ", out total, idSql).Tables[0];
            XSSFWorkbook workbook = null;
            MemoryStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {

                workbook = new XSSFWorkbook();
                ms = new MemoryStream();
                sheet = workbook.CreateSheet();
                headerRow = (XSSFRow)sheet.CreateRow(0);
                foreach (DataColumn column in dt.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= dt.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
                // _log.WriteLog($"导出会员");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message);
                Logger.Warn(ex.StackTrace);
                //  return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
            return ms;
        }
    }
}
