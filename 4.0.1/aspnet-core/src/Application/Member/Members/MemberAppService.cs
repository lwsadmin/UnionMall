﻿using System;
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
using UnionMall.Sessions;
using UnionMall.Business;
using UnionMall.Enum;
using UnionMall.Statistics;
//using UnionMall.SystemSet;

namespace UnionMall.Member
{
    public class MemberAppService : ApplicationService, IMemberAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IAbpSession _AbpSession;
        private readonly ICommonAppService _comServices;
        private readonly IRepository<UnionMall.Entity.Member, long> _Repository;
        private readonly ISessionAppService _sessionAppService;
        private readonly IRechargeNoteAppService _reAppService;
        private readonly IChainStoreAppService _storeServices;
        // private readonly ILogAppService _log;
        public MemberAppService(ISqlExecuter sqlExecuter, IRepository<UnionMall.Entity.Member, long> Repository,
            IAbpSession AbpSession, ISessionAppService sessionAppService, IChainStoreAppService storeServices, ICommonAppService comServices
       , IRechargeNoteAppService reAppService
            )
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _comServices = comServices;
            _sessionAppService = sessionAppService;
            _storeServices = storeServices;
            _reAppService = reAppService;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;

            // _log = log;
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {

            string idSql = string.Empty;
            if ((where == "" || where.IndexOf("c.") <= 0) && where.IndexOf("BusinessId") < 0)
            {
                idSql = $"SELECT rows FROM sysindexes WHERE id = OBJECT_ID('dbo.TMember') AND indid < 2";
            }
            where = where.Replace("*.", "m.").Replace("c.", "m.");
            // int[] chainstore = { 6,16,17,18,19,20,21 };
            //int[] member = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
            //    18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
            //    36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55,
            //    56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76,
            //    77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90,
            //    91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };


            //DataTable t = _sqlExecuter.ExecuteDataSet("select id,regtime from tmember").Tables[0];
            //foreach (DataRow item in t.Rows)
            //{
            //    string s = $"update TMember set RegTime=DATEADD(day,{new Random().Next(0, 100)}, GETDATE()) ,LevelId={new Random().Next(0,4 )}" +
            //        $" where id={item["id"]}";
            //    _sqlExecuter.Execute(s);
            //}
            if (string.IsNullOrEmpty(table))
            {

                table = $@"select m.id,stuff(m.FullName,2,1,'*') FullName,stuff(m.WechatName,2,1,'*') WechatName,
                 m.HeadImg,m.Sex,stuff(m.CardID,8,4,'****') CardID,
                stuff(m.Mobile,8,4,'****') Mobile,m.Balance,m.Integral,
                convert(nvarchar(100),m.RegTime,20) RegTime,l.Title,c.Name StoreName from dbo.TMember  m
                left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";

                //                table = $@"select m.id,stuff(m.FullName,2,1,'*') FullName,stuff(m.WechatName,2,1,'*') WechatName,
                //m.HeadImg,m.Sex,stuff(m.CardID,8,4,'****') CardID,
                // stuff(m.Mobile,8,4,'****') Mobile,m.Balance,m.Integral,
                // convert(nvarchar(100),m.RegTime,20) RegTime,l.Title,c.Name StoreName from dbo.TMember  m
                //,dbo.TMemberLevel l ,dbo.TChainStore c  where 1=1  and m.chainstoreId=c.id and m.levelid=l.id";
            }
            string pageTable = $@"select T.*,l.Title,c.Name StoreName from (
select m.id,m.TenantId,stuff(m.FullName,2,1,'*') FullName,stuff(m.WechatName,2,1,'*') WechatName,m.HeadImg,
m.Sex,stuff(m.CardID,8,4,'****') CardID,m.chainstoreid,m.levelid,
 stuff(m.Mobile,8,4,'****') Mobile,m.Balance,m.Integral, convert(nvarchar(100),m.RegTime,20) RegTime
  from dbo.TMember  m where id in(
select m.id from tmeMber m where 1=1  {where} order by id OFFSET {(pageIndex - 1) * pageSize} ROW FETCH NEXT {pageSize} ROWS only
)
) T
left join dbo.TMemberLevel l on T.levelId=l.id 
left join dbo.TChainStore c on T.chainstoreId=c.id";
            table += where.Replace("*", "m");

            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total, idSql, pageTable);
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
                // LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
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
        [RemoteService(IsEnabled = false)]
        public async Task<JsonResult> Import(IFormFile flie)
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
                    await _Repository.InsertAsync(member);

                }
                return new JsonResult(new { succ = true, msg = msg });
            }
            catch (Exception ex)
            {
                Logger.Warn("---------------------------" + ex.StackTrace);
                return new JsonResult(new { succ = false, msg = ex.Message });
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task<MemoryStream> ExportToExcel(string where)
        {
            string sql = $@"select  m.FullName 姓名,stuff(m.WechatName,2,1,'*') 微信名,
 case m.Sex  when 0 then '男' else '女' end 性别, stuff(m.CardID,8,4,'****') 卡号,stuff(m.Mobile,8,4,'****') 手机号,m.Balance 余额
,m.Integral 积分,convert(nvarchar(100),m.RegTime,20)  注册时间,l.Title 等级,b.BusinessName 所属商户,
c.Name  所属门店 from dbo.TMember m left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TBusiness b on
m.businessId=b.Id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";

            sql += where.Replace("*", "m");
            // DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
            return await _comServices.DataTableToExcel(sql);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.Member> GetEntity(long id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == id);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<Entity.Member> GetEntity(string cardId)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.CardID == cardId);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<CardCoreDto> GetCardCore(string cardId)
        {
            var m = await _Repository.FirstOrDefaultAsync(c => c.CardID == cardId);
            if (m == null)
            {
                return null;
            }
            CardCoreDto dto = m.MapTo<CardCoreDto>();
            dto.BirthDay = m.BirthDay.ToString("yyyy-MM-dd");
            dto.RegTime = m.RegTime.ToString("yyyy-MM-dd HH:MM");
            dto.Level = _sqlExecuter.ExecuteDataSet
                ($"select Title from dbo.TMemberLevel where Id={m.LevelId}")
                .Tables[0].Rows[0][0].ToString();
            return dto;
        }

        //[Abp.Domain.Uow.UnitOfWork]
        [RemoteService(IsEnabled = false)]
        public async Task<JsonResult> MemberRecharge(Entity.RechargeNote note)
        {

            var m = _Repository.FirstOrDefault(c => c.Id == note.MemberId);
            if (m == null)
            {
                return new JsonResult(new { succ = false, msg = L("NotExist{0}!", L("Member") + L("Records")) });
            }

            var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
            var store = await _storeServices.GetByIdAsync(UserInfo.User.ChainStoreId);
            // Logger.Warn("11111111111111111111111111111111111111111" + _comServices.GetBillNumber(OrderNumberType.OFQ));
            if (store.AvailableValue < note.Value)
            {
                return new JsonResult(new { succ = false, msg = L("Store") + L("Usable") + L("Balance") + L("NEnough") + "!" });
            }
            note.TenantId = UserInfo.Tenant.Id;
            note.Balance = m.Balance + note.Value;
            note.BusinessId = store.BusinessId;
            note.BillNumber = _comServices.GetBillNumber(OrderNumberType.OFMR);
            note.UserAccount = UserInfo.User.UserName;

            m.Balance += note.Value;
            await _Repository.UpdateAsync(m);
            await _reAppService.Create(note);
            return new JsonResult(new { succ = true, msg = L("Recharge") + L("Success") + "!" });
        }

        public async Task CardCoreEdit(string column, string card, long id, string value)
        {
            string sql = $@"update tmember set {column}={value} where id={id} and TenantId={AbpSession.TenantId} and cardid={card}";

            _sqlExecuter.Execute(sql);
        }
        [RemoteService(IsEnabled = false)]
        public async Task<DataTable> IndexMember(string where)
        {

            string sql = $@"select top 15 CONVERT(nvarchar(100),RegTime,23) DAY,
 M=count(case when Sex=0 then '男' end),
 W=count(case when Sex=1 then '女' end) 
 from tmember m
 where 1=1 and {where.Replace("*","m")}
  group by   CONVERT(nvarchar(100),RegTime,23)
  
  order by CONVERT(nvarchar(100),RegTime,23) desc";
            DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];
            return dt;
            // throw new NotImplementedException();
        }
    }
}
