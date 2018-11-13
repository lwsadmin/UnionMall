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

namespace UnionMall.Member
{
    public class MemberAppService : ApplicationService, IMemberAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IAbpSession _AbpSession;
        private readonly ICommonAppService _comServices;
        private readonly IRepository<UnionMall.Entity.Member, long> _Repository;
        public MemberAppService(ISqlExecuter sqlExecuter, IRepository<UnionMall.Entity.Member, long> Repository,
            IAbpSession AbpSession, ICommonAppService comServices)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
            _comServices = comServices;


        }
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select m.id,m.FullName,m.WechatName,m.TenantId,m.levelId,m.HeadImg,m.Sex,m.CardID,m.Mobile,m.Balance,m.Integral,
convert(nvarchar(100),m.RegTime,20) RegTime,m.businessId,m.chainStoreId,l.Title,b.BusinessName,c.Name StoreName from dbo.TMember m
left join dbo.TMemberLevel l on m.levelId=l.id left join dbo.TBusiness b on
m.businessId=b.Id left join dbo.TChainStore c on m.chainstoreId=c.id where 1=1";
            }
            table += where.Replace("*", "m");
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
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

        public JsonResult Import(IFormFile flie)
        {

            string msg = string.Empty;
            DataTable dt = _comServices.ExcelToDataTable(flie, out msg);
            foreach (DataRow row in dt.Rows)
            {
                Entity.Member member = new Entity.Member();
                member.TenantId = _AbpSession.TenantId;
                member.CardID = row["会员卡号"].ToString();
                member.FullName = row["姓名"].ToString();

                DataTable levelTable = _sqlExecuter.ExecuteDataSet($"select id from TMemberLevel l where l.TenantId={_AbpSession.TenantId}" +
                     $" and l.Title='{row["会员等级"]}'").Tables[0];

                member.LevelId = (long)levelTable.Rows[0][0];

                DataTable storeTable = _sqlExecuter.ExecuteDataSet($"select id,BusinessId from TChainStore c where c.TenantId={_AbpSession.TenantId}" +
     $" and c.Name='{row["归属门店"]}'").Tables[0];
                member.ChainStoreId = (long)storeTable.Rows[0]["id"];
                member.BusinessId = (long)storeTable.Rows[0]["BusinessId"];
                if (row["性别"].ToString() == "男")
                    member.Sex = 0;
                else
                    member.Sex = 1;


                member.Integral = Convert.ToDecimal(row["可用积分"]);
                member.Balance = Convert.ToDecimal(row["可用余额"]);
                member.Mobile = row["手机号"].ToString();
                member.PassWord = member.Mobile;
                if (row["推荐人卡号"] != null && row["推荐人卡号"].ToString() != "")
                {
                    var m = _Repository.FirstOrDefault(c => c.CardID == row["推荐人卡号"].ToString() && c.TenantId == _AbpSession.TenantId);
                    if (m != null)
                    {
                        member.ReferrerID = m.Id;
                    }
                }

                member.RegTime = DateTime.Now;
                member.Address = row["详细地址"].ToString();
                member.Email = row["电子邮件"].ToString();

                _Repository.Insert(member);
            }
            return new JsonResult(new { succ = true, msg = msg });


        }
    }
}
